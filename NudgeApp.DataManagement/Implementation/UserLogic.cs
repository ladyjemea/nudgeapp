namespace NudgeApp.DataManagement.Implementation
{
    using System.Security.Cryptography;
    using System.Text;
    using NudgeApp.Common.Enums;
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

        public bool CreateUser(string userName, string password, string name, string email, string address)
        {
            var user = this.UserRepository.GetUser(userName);

            if (user != null)
            {
                return false;
            }
            var passwordHash = this.HashPassword(password);

            this.UserRepository.CreateUser(userName, passwordHash, name, email, address);
            return true;
        }

        public bool CheckPassword(string userName, string password)
        {
            var passwordHash = this.HashPassword(password);

            var user = this.UserRepository.GetUser(userName);

            if (user == null)
                return false;

            var actualHash = user.PasswordHash;

            if (passwordHash != actualHash)
            {
                return false;
            }

            return true;
        }

        public void UpdateUserPreferences(string userName, TransportationType preferedTravelType)
        {
            var user = this.UserRepository.GetUser(userName);
            var preferences = this.PreferencesRepository.GetPreferences(user.Id) ?? this.PreferencesRepository.AddPreferences(user.Id);

            preferences.PreferedTransportationType = preferedTravelType;

            this.PreferencesRepository.UpdatePreferences(preferences);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
