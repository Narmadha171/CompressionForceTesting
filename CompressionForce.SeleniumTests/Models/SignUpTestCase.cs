namespace CompressionForce.SeleniumTests.Models
{
    public class SignUpTestCase
    {
        public string TestName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ExpectedResult { get; set; }

        public SignUpTestCase()
        {
            TestName = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            ExpectedResult = string.Empty;
        }

        public bool IsPasswordMatch()
        {
            return Password == ConfirmPassword;
        }
    }
}
