namespace CompressionForce.SeleniumTests.Models
{
    public class LoginTestCase
    {
        public string TestName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ExpectedResult { get; set; }

        public LoginTestCase()
        {
            TestName = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ExpectedResult = string.Empty;
        }

        public string GetLoginIdentifier()
        {
            return !string.IsNullOrWhiteSpace(Username) ? Username : Email;
        }
    }
}
