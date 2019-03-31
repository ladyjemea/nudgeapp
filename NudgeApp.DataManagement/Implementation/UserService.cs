﻿namespace NudgeApp.DataManagement.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using RestSharp;

    public class UserService : IUserService
    {
        private IUserRepository UserRepository;
        private IPreferencesRepository PreferencesRepository;

        public UserService(IUserRepository userRepository, IPreferencesRepository preferencesRepository)
        {
            this.UserRepository = userRepository;
            this.PreferencesRepository = preferencesRepository;
        }

        public IList<Guid> GetAllUserIds()
        {
            return this.UserRepository.GetAllIds().ToList();
        }

        public bool CreateUser(string userName, string password, string name, string email, string address, TransportationType travelType)
        {
            var user = this.UserRepository.GetUser(userName);

            if (user != null)
            {
                return false;
            }

            byte[] passwordHash, passwordSalt;
            GenerateHash(password, out passwordHash, out passwordSalt);

            var userId = Guid.NewGuid();
            this.UserRepository.Insert(new UserEntity
            {
                Id = userId,
                UserName = userName,
                Address = address,
                Email = email,
                Name = name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            this.UpdateUserPreferences(userId, travelType);
            return true;
        }

        public UserEntity CheckPassword(string userName, string password)
        {
            var user = this.UserRepository.GetUser(userName);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public void UpdateUserPreferences(Guid userId, TransportationType preferedTravelType)
        {
            //var user = this.UserRepository.GetUser(userName);
            var preferences = this.PreferencesRepository.GetPreferences(userId) ?? this.PreferencesRepository.AddPreferences(userId);

            preferences.PreferedTransportationType = preferedTravelType;

            this.PreferencesRepository.Update(preferences);
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

        public void GetUser()
        {
            throw new NotImplementedException();
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
            var user = this.UserRepository.GetAll().Where(u => u.Email == email).FirstOrDefault();

            Guid id;
            if (user == null)
            {
                id = this.UserRepository.Insert(new UserEntity
                {
                    Email = email,
                    Name = name,
                    Google = true
                });
            }
            else
            {
                id = user.Id;
                if (user.Google == false)
                {
                    user.Google = true;
                    this.UserRepository.Update(user);
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