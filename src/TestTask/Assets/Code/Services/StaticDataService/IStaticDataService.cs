using Code.Inventory.Config;
using Code.StaticData;
using Code.UI.Services.Window;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService
    {
        void LoadAll();
        ItemDefinition GetItemDefinition(ItemId itemId);
        GameObject GetWindowPrefab(WindowId id);
        InventoryConfig GetInventoryConfig();
    }
}