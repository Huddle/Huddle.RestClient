using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Huddle.Clients
{
    public class ServicePointManagerHelper
    {
        public static void TrustAllCertificates()
        {
            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificatesCallback;
        }

        public static bool TrustAllCertificatesCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}