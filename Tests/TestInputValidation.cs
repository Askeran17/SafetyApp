// Tests/TestInputValidation.cs
using NUnit.Framework;

[TestFixture]
public class TestInputValidation {
    [Test]
    public void TestForSQLInjection() {
        string suspiciousInput = "' OR '1'='1";
        bool isValid = SafeInputValidator.IsValidUsername(suspiciousInput);
        Assert.IsFalse(isValid, "SQL injection-like username should not be valid.");
    }

    [Test]
    public void TestForXSS() {
        string xssInput = "<script>alert('XSS');</script>";
        string sanitized = SafeInputValidator.Sanitize(xssInput);
        Assert.IsFalse(sanitized.Contains("<script>"), "XSS content was not properly sanitized.");
    }
}
