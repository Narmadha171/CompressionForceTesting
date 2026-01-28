using System;
using System.IO;
using Newtonsoft.Json;
using CompressionForce.SeleniumTests.Models;

namespace CompressionForce.SeleniumTests.Utilities
{
    public static class TestDataReader
    {
        private static TestDataRoot? _cachedData;

        public static TestDataRoot LoadTestData()
        {
            if (_cachedData != null)
                return _cachedData;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var jsonPath = Path.Combine(baseDir, "TestData", "testdata.json");

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Test data file not found: {jsonPath}");

            Console.WriteLine($"Loading test data from: {jsonPath}");

            var json = File.ReadAllText(jsonPath);
            _cachedData = JsonConvert.DeserializeObject<TestDataRoot>(json) ?? new TestDataRoot();

            return _cachedData;
        }
    }
}
