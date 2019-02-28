namespace NudgeApp.DataManagement.Implementation
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public class UserLogic : IUserLogic
    {
        private IUserRepository UserRepository;
        private IPreferencesRepository PreferencesRepository;

        public UserLogic(IUserRepository userRepository, IPreferencesRepository preferencesRepository)
        {
            this.UserRepository = userRepository;
            this.PreferencesRepository = preferencesRepository;
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

            this.PreferencesRepository.UpdatePreferences(preferences);
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
    }
}
