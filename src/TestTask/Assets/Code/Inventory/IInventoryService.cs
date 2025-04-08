using System;
using System.Collections.Generic;
using Code.StaticData;

namespace Code.Inventory
{
    public interface IInventoryService
    {
        bool AddItem(ItemId id, int count);
        List<InventoryItem> GetItems();
        int GetCellCount();
        
        void ToggleAnimalState();
        void DecreaseItemCount();
        
        event Action OnInventoryChanged;
    }
}