namespace WebAPI.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application
            {
                ApplicationId = 1,
                Applicationname = "WebApp",
                ClientId = "Aa123456",
                Secret = "asdf123",
                Scopes = "read,write"
            }
        };

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
