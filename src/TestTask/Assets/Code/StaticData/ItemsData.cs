using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "StaticData/ItemData")]
    public class ItemsData : ScriptableObject
    {
        public List<ItemDefinition> Items;
    }

    [System.Serializable]
    public class ItemDefinition
    {
        public ItemId ID;
        public ItemType Type;
        public Sprite Icon;
        public int StackLimit;
        public string Name;
    }
}