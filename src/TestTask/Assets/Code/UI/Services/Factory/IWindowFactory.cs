using Code.UI.Services.Window;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
    public interface IWindowFactory
    {
        void SetUIRoot(RectTransform uiRoot);
        BaseWindow CreateWindow(WindowId windowId);
    }
}