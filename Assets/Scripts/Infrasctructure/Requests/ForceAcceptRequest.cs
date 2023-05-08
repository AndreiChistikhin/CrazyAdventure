using UnityEngine.Networking;

namespace Infrasctructure.Requests
{
    public class ForceAccept : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
