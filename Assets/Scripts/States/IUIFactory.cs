using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public interface IUIFactory
{
    UniTask CreateUIRoot();
    UniTask CreateLoseGameMenu();
    UniTask CreateWinGameMenu();
}