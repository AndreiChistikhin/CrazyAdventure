using Cysharp.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        UniTask CreateLoseGameMenu();
        UniTask CreateWinGameMenu();
    }
}