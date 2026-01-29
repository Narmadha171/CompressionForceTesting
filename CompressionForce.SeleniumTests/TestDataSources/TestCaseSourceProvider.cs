using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using CompressionForce.SeleniumTests.Utilities;

namespace CompressionForce.SeleniumTests.TestDataSources
{
    public static class TestCaseSourceProvider
    {
        public static IEnumerable<TestCaseData> GetLoginTestData()
        {
            var data = TestDataReader.LoadTestData();
            // Merge Username, Email, and Format tests for the Login test method
            var allTests = data.LoginPageUsernameTest
                .Concat(data.LoginPageEmailTest)
                .Concat(data.LoginPageFormatTest);

            foreach (var item in allTests)
            {
                yield return new TestCaseData(item).SetName(item.TestName);
            }
        }

        public static IEnumerable<TestCaseData> GetSignUpTestData()
        {
            var data = TestDataReader.LoadTestData();
            // Merge User and Email tests for the SignUp test method
            var allTests = data.SignUpUserTest.Concat(data.SignUpEmailTest);

            foreach (var item in allTests)
            {
                yield return new TestCaseData(item).SetName(item.TestName);
            }
        }
    }
}