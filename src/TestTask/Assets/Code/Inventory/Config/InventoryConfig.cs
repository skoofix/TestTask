using UnityEngine;

namespace Code.Inventory.Config
{
    [CreateAssetMenu(fileName = "InventoryConfig", menuName = "StaticData/InventoryConfig")]
    public class InventoryConfig : ScriptableObject
    {
        public int CellCount = 20;
        public GameObject CellPrefab;
    }
}