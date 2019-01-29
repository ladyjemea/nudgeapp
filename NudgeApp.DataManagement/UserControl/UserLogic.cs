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

        public void CreateUser(string userName, string password)
        {
            var passwordHash = "";
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
                passwordHash = builder.ToString();
            }

            this.UserRepository.CreateUser(userName, passwordHash);
        }
    }
}
