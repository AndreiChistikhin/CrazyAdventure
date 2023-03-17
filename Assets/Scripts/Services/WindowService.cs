using System;
using System.Collections.Generic;
using Configs;
using Services.Interfaces;

namespace Services
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private Dictionary<WindowId, Action> _windowCreationMethods = new Dictionary<WindowId, Action>(); 

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _windowCreationMethods[WindowId.LoseGame] = () => _uiFactory.CreateLoseGameMenu();
            _windowCreationMethods[WindowId.WinGame] = () => _uiFactory.CreateWinGameMenu();
        }

        public void Open(WindowId windowId)
        {
            _windowCreationMethods[windowId]?.Invoke();
        }
    }
}