using System;
using Code.UI.Services.Window;
using UnityEngine;

namespace Code.UI.Windows.Config
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId Id;
        public GameObject Prefab;
    }
}