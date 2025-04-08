using System;
using System.Collections.Generic;
using Code.Services.StaticDataService;
using Code.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IStaticDataService _staticDataService;
        private List<InventoryItem> _items = new();
        
        public event Action OnInventoryChanged;

        public InventoryService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public void DecreaseItemCount()
        {
            if (IsInventoryEmpty())
                return;
            
            InventoryItem item = GetRandomItem();
            
            UpdateItemCount(item, -1);
        }

        public void ToggleAnimalState()
        {
            List<InventoryItem> animals = _items.FindAll(item => item.State.HasValue);
            
            if (animals.Count == 0)
                return;
            
            InventoryItem selectedAnimal = GetRandomItem(animals);
            AnimalState newState = GetToggledState(selectedAnimal);
            
            if (TryMergeWithExistingStacks(selectedAnimal, newState))
                return;

            selectedAnimal.State = newState;
            OnInventoryChanged?.Invoke();
        }

        public bool AddItem(ItemId id, int count)
        {
            if (count <= 0) 
                return true;

            ItemDefinition itemDef = GetItemDefinition(id);
            int originalCount = count;
    
            count = AddToExistingStacks(id, count, itemDef);
            count = AddNewStacks(id, count, itemDef);

            bool allAdded = count == 0;
    
            if (originalCount - count > 0)
                OnInventoryChanged?.Invoke();

            return allAdded;
        }
        
        public List<InventoryItem> GetItems() => 
            _items;

        public int GetCellCount() => 
            _staticDataService.GetInventoryConfig().CellCount;

        private void UpdateItemCount(InventoryItem item, int change)
        {
            item.Count += change;
            if (item.Count <= 0)
                _items.Remove(item);
            OnInventoryChanged?.Invoke();
        }

        private bool IsInventoryEmpty() => 
            _items.Count == 0;

        private InventoryItem GetRandomItem(List<InventoryItem> items = null) =>
            (items ?? _items)[Random.Range(0, (items ?? _items).Count)];

        private bool TryMergeWithExistingStacks(InventoryItem selectedAnimal, AnimalState newState)
        {
            List<InventoryItem> targetStacks = _items
                .FindAll(item => item.ID == selectedAnimal.ID && item.State == newState);

            foreach (InventoryItem stack in targetStacks)
            {
                ItemDefinition itemDef = _staticDataService.GetItemDefinition(stack.ID);

                int space = itemDef.StackLimit - stack.Count;

                if (space > 0)
                {
                    stack.Count++;
                    selectedAnimal.Count--;

                    if (selectedAnimal.Count <= 0)
                        _items.Remove(selectedAnimal);

                    OnInventoryChanged?.Invoke();

                    return true;
                }
            }
            return false;
        }

        private static AnimalState GetToggledState(InventoryItem selectedAnimal)
        {
            return selectedAnimal.State == AnimalState.Healthy
                ? AnimalState.Wounded
                : AnimalState.Healthy;
        }

        private int AddNewStacks(ItemId id, int count, ItemDefinition itemDef)
        {
            while (count > 0 && _items.Count < _staticDataService.GetInventoryConfig().CellCount)
            {
                int toAdd = Mathf.Min(itemDef.StackLimit, count);
                AnimalState? randomState = itemDef.Type == ItemType.Animal
                    ? GetRandomAnimalState()
                    : null;
                var newStack = new InventoryItem { ID = id, Count = toAdd, State = randomState };
                _items.Add(newStack);
                count -= toAdd;
            }
            return count;
        }

        private int AddToExistingStacks(ItemId id, int count, ItemDefinition itemDef)
        {
            List<InventoryItem> existingStacks = _items.FindAll(item => item.ID == id);

            foreach (InventoryItem stack in existingStacks)
            {
                int space = itemDef.StackLimit - stack.Count;

                if (space > 0)
                {
                    int toAdd = Mathf.Min(space, count);
                    stack.Count += toAdd;
                    count -= toAdd;
                    if (count == 0) break;
                }
            }
            return count;
        }

        private ItemDefinition GetItemDefinition(ItemId id) => 
            _staticDataService.GetItemDefinition(id);

        private AnimalState GetRandomAnimalState()
        {
            Array values = Enum.GetValues(typeof(AnimalState));
            
            return (AnimalState)values.GetValue(Random.Range(0, values.Length));
        }
    }
}