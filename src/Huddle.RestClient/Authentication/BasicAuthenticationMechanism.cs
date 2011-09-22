using System;
using System.Text;

namespace Huddle.Clients.Authentication
{
    internal class BasicAuthenticationMechanism : IRequestAuthenticationMechanism
    {
        private readonly ApiCall _apiCall;
        readonly string _username;
        readonly string _password;

        internal BasicAuthenticationMechanism(ApiCall apiCall, string username, string password)
        {
            _apiCall = apiCall;
            _username = username;
            _password = password;
        }

        public string GetAuthenticationHeader()
        {
            var token = _username + ":" + _password;
            var tokenBytes = Encoding.ASCII.GetBytes(token);
            var credentials = "Basic " + Convert.ToBase64String(tokenBytes);

            _apiCall.Log.DebugFormat("Authorization: {0} ({1})", token, credentials);
            return credentials;
        }
    }
}