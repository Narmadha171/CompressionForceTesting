using System.Collections;
using CompressionForce.SeleniumTests.Utilities;

namespace CompressionForce.SeleniumTests.TestDataSources
{
    public static class TestCaseSourceProvider
    {
        public static IEnumerable LoginByUsername()
        {
            var data = TestDataReader.LoadTestData();
            foreach (var test in data.LoginPageUsernameTest)
                yield return new object[] { test };
        }

        public static IEnumerable LoginByEmail()
        {
            var data = TestDataReader.LoadTestData();
            foreach (var test in data.LoginPageEmailTest)
                yield return new object[] { test };
        }

        public static IEnumerable LoginByFormat()
        {
            var data = TestDataReader.LoadTestData();
            foreach (var test in data.LoginPageFormatTest)
                yield return new object[] { test };
        }

        public static IEnumerable SignUpByUser()
        {
            var data = TestDataReader.LoadTestData();
            foreach (var test in data.SignUpUserTest)
                yield return new object[] { test };
        }

        public static IEnumerable SignUpByEmail()
        {
            var data = TestDataReader.LoadTestData();
            foreach (var test in data.SignUpEmailTest)
                yield return new object[] { test };
        }
    }
}
