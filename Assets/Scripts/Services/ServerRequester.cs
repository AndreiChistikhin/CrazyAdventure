using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class ServerRequester : IServerRequester
    {
        public string Token { get; set; }

        public async UniTask<T> Get<T>(string endPoint)
        {
            var getRequest = CreateRequest(endPoint);
            await getRequest.SendWebRequest();
            
            return JsonUtility.FromJson<T>(getRequest.downloadHandler.text);
        }

        public async UniTask<T> Post<T>(string endPoint, object payload)
        {
            var postRequest = CreateRequest(endPoint, RequestType.Post, payload);
            await postRequest.SendWebRequest();

            return JsonUtility.FromJson<T>(postRequest.downloadHandler.text);
        }

        private static UnityWebRequest CreateRequest(string path, RequestType type = RequestType.Get,
            object data = null)
        {
            var request = new UnityWebRequest(path, type.ToString());

            if (data != null)
            {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();

            return request;
        }
    }

    public enum RequestType
    {
        Get = 0,
        Post = 1,
        Put = 2
    }
}