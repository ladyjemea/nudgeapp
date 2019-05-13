namespace NudgeApp.DataManagement.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using RestSharp;

    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IPreferencesRepository PreferencesRepository;
        private readonly IActualPreferencesRepository ActualPreferencesRepository;
        private readonly IAccountRepository AccountRepository;

        public UserService(IUserRepository userRepository, IAccountRepository accountRepository,IPreferencesRepository preferencesRepository, IActualPreferencesRepository actualPreferencesRepository)
        {
            this.UserRepository = userRepository;
            this.PreferencesRepository = preferencesRepository;
            this.ActualPreferencesRepository = actualPreferencesRepository;
            this.AccountRepository = accountRepository;
        }

        public IList<Guid> GetAllUserIds()
        {
            return this.UserRepository.GetAllIds().ToList();
        }

        public ActualPreferencesEntity GetActualPreferences(Guid userId)
        {
            var result = this.ActualPreferencesRepository.GetAll().FirstOrDefault(a => a.UserId == userId);

            if (result == null)
                result = new ActualPreferencesEntity();

            return result;
        }

        public void SetActualPreferences(Guid userId, ActualPreferencesEntity preferences)
        {

            var exists = this.ActualPreferencesRepository.Get(preferences.Id);

            if (exists == null)
            {
                preferences.UserId = userId;
                this.ActualPreferencesRepository.Insert(preferences);
            }
            else
                this.ActualPreferencesRepository.Update(preferences);
        }

        public bool CreateUser(string password, string name, string email, string address, TransportationType transportationType)
        {
            var user = this.UserRepository.GetUser(email);

            if (user != null)
            {
                return false;
            }

            byte[] passwordHash, passwordSalt;
            GenerateHash(password, out passwordHash, out passwordSalt);

            var accountId = Guid.NewGuid();
            this.AccountRepository.InsertWIthNoSave(new AccountEntity
            {
                Id = accountId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            var userId = Guid.NewGuid();
            this.UserRepository.InsertWIthNoSave(new UserEntity
            {
                Id = userId,
                Address = address,
                Email = email,
                Name = name,
                AccountId = accountId
            });

            this.UpdatePreferences(userId, new PreferencesDto { TransportationType = transportationType });

            return true;
        }

        public UserEntity CheckPassword(string userName, string password)
        {
            var user = this.UserRepository.GetUser(userName);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.Account.PasswordHash, user.Account.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public PreferencesDto GetPreferences(Guid userId)
        {
            var preferences = this.PreferencesRepository.GetAll().FirstOrDefault(p => p.UserId == userId);

            if (preferences == null)
            {
                return new PreferencesDto
                {
                    MinTemperature = -50,
                    MaxTemperature = 50,
                    RainyTrip = true,
                    SnowyTrip = true,
                    TransportationType = TransportationType.Unknown
                };
            }

            return new PreferencesDto
            {
                MaxTemperature = preferences.MaxTemperature,
                MinTemperature = preferences.MinTemperature,
                RainyTrip = preferences.RainyTrip,
                SnowyTrip = preferences.SnowyTrip,
                TransportationType = preferences.TransportationType
            };
        }

        public void UpdatePreferences(Guid userId, PreferencesDto preferencesDto)
        {
            var preferences = this.PreferencesRepository.GetAll().FirstOrDefault(p => p.UserId == userId);

            var exists = true;
            if (preferences == null)
            {
                exists = false;
                preferences = new PreferencesEntity
                {
                    UserId = userId
                };
            }

            preferences.MinTemperature = preferencesDto.MinTemperature;
            preferences.MaxTemperature = preferencesDto.MaxTemperature;
            preferences.TransportationType = preferencesDto.TransportationType;
            preferences.RainyTrip = preferencesDto.RainyTrip;
            preferences.SnowyTrip = preferencesDto.SnowyTrip;

            if (exists)
                this.PreferencesRepository.Update(preferences);
            else
                this.PreferencesRepository.Insert(preferences);
        }

        private static void GenerateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public Guid VerifyGoogle(string id, string tokenId)
        {
            var client = new RestClient("https://www.googleapis.com");
            var request = new RestRequest("oauth2/v3/tokeninfo", Method.GET);
            request.AddParameter("id_token", tokenId);

            var response = client.Execute<TokenInfo>(request);

            if (response.Data.Sub == id)
            {
                var userId = this.GoogleLogin(response.Data.Email, response.Data.Name);

                return userId;
            }

            return Guid.Empty;
        }

        private Guid GoogleLogin(string email, string name)
        {
            var user = this.UserRepository.GetAll().Include(u => u.Account).Where(u => u.Email == email).FirstOrDefault();

            Guid id;
            if (user == null)
            {
                var accountId = Guid.NewGuid();
                this.AccountRepository.InsertWIthNoSave(new AccountEntity
                {
                    Id = accountId,
                    Google = true
                });

                id = this.UserRepository.Insert(new UserEntity
                {
                    Email = email,
                    Name = name,
                    AccountId = accountId
                });
            }
            else
            {
                id = user.Id;
                if (user.Account.Google == false)
                {
                    user.Account.Google = true;
                    this.AccountRepository.Update(user.Account);
                }
            }

            return id;
        }

        private class TokenInfo
        {
            public string Sub { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            /* + other info  */
        }
    }
}
