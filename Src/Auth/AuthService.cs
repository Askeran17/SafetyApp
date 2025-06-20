using SafeVault.Auth;
using BCrypt.Net;

namespace SafeVault
{
    public class AuthService
    {
        private readonly DatabaseHandler db = new DatabaseHandler();

        /// <summary>
        /// Регистрирует нового пользователя с хешированным паролем и ролью "user"
        /// </summary>
        public bool RegisterUser(string username, string email, string plainPassword)
        {
            if (!SafeInputValidator.IsValidUsername(username) || !SafeInputValidator.IsValidEmail(email))
                return false;

            string hashed = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return db.CreateUser(username, email, hashed, "user"); // можно заменить "user" на "admin" при необходимости
        }

        /// <summary>
        /// Проверяет логин и возвращает true, если пароль совпадает с хешем в БД
        /// </summary>
        public bool Authenticate(string username, string password)
        {
            var user = db.GetUserByUsername(username);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.HashedPassword);
        }
    }
}

