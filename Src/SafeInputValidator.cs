// SafeInputValidator.cs
public static class SafeInputValidator {
    public static string Sanitize(string input) {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        
        string sanitized = System.Net.WebUtility.HtmlEncode(input);
        return sanitized.Trim();
    }

    public static bool IsValidEmail(string email) {
        return System.Text.RegularExpressions.Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
        );
    }

    public static bool IsValidUsername(string username) {
        return System.Text.RegularExpressions.Regex.IsMatch(
            username,
            @"^[a-zA-Z0-9_]{3,20}$"
        );
    }
}
