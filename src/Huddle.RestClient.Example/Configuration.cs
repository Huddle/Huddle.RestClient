using log4net.Config;

namespace Huddle.Clients.Example
{
    public static class Configuration
    {
        /// set this to the ID of the user that you are using for testing.
        internal const string TestUserId = "13470075";

        /// set this to a valid folder ID for the user above.  
        /// NB this is used to hack URIs and is **NOT** the correct approach to use the
        /// Huddle files API.
        internal const string TestFolderId = "15549923";

        /// set this to the full domain and protocol for your API request.
        internal const string BaseUrl = "http://api.huddle.dev/";
        internal const string BaseUrlSsl = "https://api.huddle.dev/";

        /// set this to the OAuth2 access token that you wish to use for testing.
        internal static readonly string OAuth2Token = "{'iss':'auth.huddle.dev', 'exp': 1986681600, 'urn:huddle.claims.userid': 13470075}";

        internal static readonly string GetTestUserUri = string.Format(BaseUrl + "users/{0}", TestUserId);
        internal static readonly string GetSecureTestUserUri = string.Format(BaseUrlSsl + "users/{0}", TestUserId);

        internal static readonly string ExampleCreateDocUri = string.Format(BaseUrl + "folders/{0}/documents", TestFolderId);

        static Configuration()
        {
            // need to get the log4net config in somewhere.
            BasicConfigurator.Configure();
        }

    }
}