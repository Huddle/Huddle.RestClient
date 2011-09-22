namespace Huddle.RestClient.Authentication
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;

    public interface IWebClient
    {
        string Post(string uri, NameValueCollection values);
    }

    public class HuddleWebClient : IWebClient
    {
        public string Post(string uri, NameValueCollection values)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, errors) => true;
            
            var client = new WebClient();
            var response = client.UploadValues(uri, values);
            return Encoding.ASCII.GetString(response);
        }
    }
}