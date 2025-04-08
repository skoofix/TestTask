using System;
using System.Collections.Generic;
using System.Linq;
using Code.Inventory.Config;
using Code.StaticData;
using Code.UI.Services.Window;
using Code.UI.Windows.Config;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<ItemId, ItemDefinition> _itemsById;
        private Dictionary<WindowId, GameObject> _windowPrefabsById;
        private InventoryConfig _inventoryConfig;

        public void LoadAll()
        {
            LoadItemsConfig();
            LoadWindows();
            LoadInventoryConfig();
        }

        public InventoryConfig GetInventoryConfig() => _inventoryConfig;
        
        public ItemDefinition GetItemDefinition(ItemId itemId) =>
            _itemsById.TryGetValue(itemId, out ItemDefinition definition)
                ? definition
                : throw new Exception($"Orb definition for {itemId} was not found");
        
        public GameObject GetWindowPrefab(WindowId id) =>
            _windowPrefabsById.TryGetValue(id, out GameObject prefab)
                ? prefab
                : throw new Exception($"Prefab config for window {id} was not found");
        
        
        private void LoadItemsConfig()
        {
            ItemsData config = Resources.Load<ItemsData>("ItemData");

            _itemsById = config.Items.ToDictionary(
                item => item.ID,
                item => item);
        }
        
        private void LoadInventoryConfig()
        {
            _inventoryConfig = Resources.Load<InventoryConfig>("InventoryConfig");
        }
        
        private void LoadWindows()
        {
            _windowPrefabsById = Resources
                .Load<WindowsConfig>("windowConfig")
                .WindowConfigs
                .ToDictionary(x => x.Id, x => x.Prefab);
        }
    }
}