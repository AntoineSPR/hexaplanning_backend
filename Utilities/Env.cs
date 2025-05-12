﻿namespace Procrastinator.Utilities
{
    public static class Env
    {
        public static string GetEnv(string key, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return value;
        }

        public static string GetEnv(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? string.Empty;
        }

        public static string CONNECTION_STRING => GetEnv(nameof(CONNECTION_STRING), "Host=localhost;Port=5432;Database=procrastinator;Username=postgres;Password=postgres");
        public static string API_BACK_URL => GetEnv(nameof(API_BACK_URL), "https://localhost:7113");
        public static string API_FRONT_URL => GetEnv(nameof(API_FRONT_URL), "http://localhost:4200");
        public static string JWT_KEY => GetEnv(nameof(JWT_KEY), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV31");
        public static int TOKEN_VALIDITY_DAYS
        {
            get
            {
                if (int.TryParse(GetEnv(nameof(TOKEN_VALIDITY_DAYS)), out var days))
                {
                    return days;
                }
                return 7;
            }
        }
    }
}
