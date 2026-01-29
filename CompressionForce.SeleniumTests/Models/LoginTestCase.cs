namespace CompressionForce.SeleniumTests.Models
{
    public class LoginTestCase
    {
        public string TestName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;

        /// <summary>
        /// Returns the Username if available, otherwise returns the Email.
        /// </summary>
        public string GetLoginIdentifier()
        {
            return !string.IsNullOrWhiteSpace(Username) ? Username : Email;
        }
    }
}