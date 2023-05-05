using Cysharp.Threading.Tasks;
using Services.Interfaces;

namespace Services
{
    public interface IServerRequester : IService
    {
        string Token { get; set; }
        UniTask<T> Get<T>(string endPoint);
        UniTask<T> Post<T>(string endPoint, object payload);
    }
}