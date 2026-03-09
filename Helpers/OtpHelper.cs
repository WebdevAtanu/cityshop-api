namespace cityshop_api.Helpers
{
    public static class OtpHelper
    {
        public static string GenerateOtp()
        {
            Random random = new();
            return random.Next(100000, 999999).ToString();
        }
    }
}
