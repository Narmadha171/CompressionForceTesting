using System.Collections.Generic;

namespace CompressionForce.SeleniumTests.Models
{
    public class TestDataRoot
    {
        // Matches "BASE_URL" in JSON
        public string BASE_URL { get; set; } = string.Empty;

        // Login Test Lists
        public List<LoginTestCase> LoginPageUsernameTest { get; set; } = new();
        public List<LoginTestCase> LoginPageEmailTest { get; set; } = new();
        public List<LoginTestCase> LoginPageFormatTest { get; set; } = new();

        // SignUp Test Lists
        public List<SignUpTestCase> SignUpUserTest { get; set; } = new();
        public List<SignUpTestCase> SignUpEmailTest { get; set; } = new();
    }
}