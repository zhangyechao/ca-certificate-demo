using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CusSSLLib
{
    public static class CaHelper
    {
        internal static string CA_DATA = System.Text.Encoding.UTF8.GetString(CAResource.ca).Replace("-----BEGIN CERTIFICATE-----", "")
                     .Replace("-----END CERTIFICATE-----", "")
                     .Replace("\r", "")
                     .Replace("\n", "");

        public static bool Valid(X509Certificate2 certificate, X509Chain chain, SslPolicyErrors policy)
        {
            // the root certificate
            var validRootCertificates = new[]
            {
                 Convert.FromBase64String(CA_DATA),
            };

            foreach (var element in chain.ChainElements)
            {
                foreach (var status in element.ChainElementStatus)
                {
                    if (status.Status == X509ChainStatusFlags.UntrustedRoot)
                    {
                        if (validRootCertificates.Any(x => x.SequenceEqual(element.Certificate.RawData)))
                        {
                            continue;
                        }
                    }

                    return false;
                }
            }

            return true;
        }
    }
}