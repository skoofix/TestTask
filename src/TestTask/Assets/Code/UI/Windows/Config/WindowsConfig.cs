using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Windows.Config
{
    [CreateAssetMenu(fileName = "windowConfig", menuName = "Window Config")]
    public class WindowsConfig : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}