namespace NudgeApp.DataManagement.UserControl
{
    using System.Security.Cryptography;
    using System.Text;
    using NudgeApp.Data.Repositories.User;

    public class UserLogic : IUserLogic
    {
        private IUserRepository UserRepository;

        public UserLogic(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public bool CreateUser(string userName, string password)
        {
            var user = this.UserRepository.GetUser(userName);

            if (user != null)
            {
                return false;
            }
            var passwordHash = this.HashPassword(password);
            
            this.UserRepository.CreateUser(userName, passwordHash);
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

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

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
