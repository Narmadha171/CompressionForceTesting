namespace CompressionForce.SeleniumTests.Models
{
    public class SignUpTestCase
    {
        public string TestName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;

        public bool IsPasswordMatch()
        {
            return Password == ConfirmPassword;
        }
    }
}