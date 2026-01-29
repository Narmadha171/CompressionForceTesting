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
            if (_cachedData != null) return _cachedData;

            // AppDomain.CurrentDomain.BaseDirectory targets bin\Debug\net10.0\
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // FIX: You MUST include "TestData" because of your folder structure
            var jsonPath = Path.Combine(baseDir, "TestData", "TestData.json");

            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"File not found at: {jsonPath}");
            }

            var json = File.ReadAllText(jsonPath);
            _cachedData = JsonConvert.DeserializeObject<TestDataRoot>(json) ?? new TestDataRoot();

            return _cachedData;
        }
    }
}