using System.Collections.Generic;

namespace CompressionForce.SeleniumTests.Models
{
    public class TestDataRoot
    {
        public string BASE_URL { get; set; }
        public List<LoginTestCase> LoginPageUsernameTest { get; set; }
        public List<LoginTestCase> LoginPageEmailTest { get; set; }
        public List<LoginTestCase> LoginPageFormatTest { get; set; }
        public List<SignUpTestCase> SignUpUserTest { get; set; }
        public List<SignUpTestCase> SignUpEmailTest { get; set; }

        public TestDataRoot()
        {
            BASE_URL = string.Empty;
            LoginPageUsernameTest = new List<LoginTestCase>();
            LoginPageEmailTest = new List<LoginTestCase>();
            LoginPageFormatTest = new List<LoginTestCase>();
            SignUpUserTest = new List<SignUpTestCase>();
            SignUpEmailTest = new List<SignUpTestCase>();
        }
    }
}
