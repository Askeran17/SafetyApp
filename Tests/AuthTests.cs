using NUnit.Framework;
using SafeVault.Auth;

namespace SafeVault.Tests
{
    [TestFixture]
    public class AuthTests
    {
        private AuthService authService;
        private AuthorizationService authzService;

        [SetUp]
        public void Setup()
        {
            authService = new AuthService();
            authzService = new AuthorizationService();
        }

        [Test]
        public void TestInvalidLogin()
        {
            bool result = authService.Authenticate("unknown_user", "invalid_password");
            Assert.IsFalse(result, "Login should fail for non-existent user.");
        }

        [Test]
        public void TestRegisterAndLoginSuccess()
        {
            string username = "testuser";
            string email = "testuser@example.com";
            string password = "StrongPass123!";

            bool registered = authService.RegisterUser(username, email, password);
            Assert.IsTrue(registered, "User should be registered successfully.");

            bool login = authService.Authenticate(username, password);
            Assert.IsTrue(login, "Login should succeed with correct credentials.");
        }

        [Test]
        public void TestAccessDeniedToAdmin()
        {
            var user = new User { Username = "regular", Role = "user" };
            bool access = authzService.HasAccess(user, "AdminDashboard");
            Assert.IsFalse(access, "User should not have access to admin resources.");
        }

        [Test]
        public void TestAdminAccessGranted()
        {
            var admin = new User { Username = "admin", Role = "admin" };
            bool access = authzService.HasAccess(admin, "AdminDashboard");
            Assert.IsTrue(access, "Admin should have access to admin resources.");
        }
    }
}

