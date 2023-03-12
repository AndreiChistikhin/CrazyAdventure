using Configs;
using Services.Interfaces;

namespace Services
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.LoseGame:
                    _uiFactory.CreateLoseGameMenu();
                    break;
                case WindowId.WinGame:
                    _uiFactory.CreateWinGameMenu();
                    break;
            }
        }
    }
}