using System.Collections.Generic;
using Code.UI.Services.Factory;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IWindowFactory _windowFactory;

        private readonly List<BaseWindow> _openedWindows = new();

        public WindowService(IWindowFactory windowFactory) =>
            _windowFactory = windowFactory;

        public void Open(WindowId windowId) => 
            _openedWindows.Add(_windowFactory.CreateWindow(windowId));

        public void Close(WindowId windowId)
        {
            BaseWindow window = _openedWindows.Find(x => x.Id == windowId);
      
            _openedWindows.Remove(window);
      
            GameObject.Destroy(window.gameObject);
        }
    }
}