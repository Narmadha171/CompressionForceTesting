namespace CompressionForce.SeleniumTests.Models
{
    public class AuthConfig
    {
        public string BaseUrl { get; set; }
        public LoginByUsername LoginByUsername { get; set; }
        public LoginByEmail LoginByEmail { get; set; }
        public SignUpData SignUp { get; set; }
    }

    public class LoginByUsername
    {
        public string ValidUsername { get; set; }
        public string InvalidUsername { get; set; }
        public string ValidPassword { get; set; }
        public string InvalidPassword { get; set; }
    }

    public class LoginByEmail
    {
        public string ValidEmail { get; set; }
        public string InvalidEmail { get; set; }
        public string ValidPassword { get; set; }
        public string InvalidPassword { get; set; }
    }

    public class SignUpData
    {
        public string ValidPassword { get; set; }
        public string InvalidEmail { get; set; }
        public string ShortUsername { get; set; }
    }
}
