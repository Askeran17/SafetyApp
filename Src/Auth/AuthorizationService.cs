namespace SafeVault.Auth
{
    public class AuthorizationService
    {
        public bool HasAccess(User user, string resource)
        {
            if (resource == "AdminDashboard" && user.Role != "admin")
                return false;

            
            return true;
        }
    }
}
