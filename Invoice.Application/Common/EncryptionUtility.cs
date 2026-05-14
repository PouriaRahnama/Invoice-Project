namespace Invoice.Application.Common
{
    public static class EncryptionUtility
    {
        public static string GetSHA256(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static string GetNewSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetNewRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }


    }
}
