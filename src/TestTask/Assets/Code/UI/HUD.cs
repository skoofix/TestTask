using System;
using Code.Inventory;
using Code.StaticData;
using Code.UI.Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _addItemButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _togggleAnimalButton;
        
        private IWindowService _windowService;
        private IInventoryService _inventoryService;

        [Inject]
        private void Construct(IWindowService windowService, IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _windowService = windowService;
        }

        private void Awake()
        {
            _inventoryButton.onClick.AddListener(OpenInventory);
            _addItemButton.onClick.AddListener(AddItem);
            _removeButton.onClick.AddListener(DecreaseItemCount);
            _togggleAnimalButton.onClick.AddListener(ToggleAnimalState);
        }

        private void OnDestroy()
        {
            _inventoryButton.onClick.RemoveListener(OpenInventory);
            _addItemButton.onClick.RemoveListener(AddItem);
            _removeButton.onClick.RemoveListener(DecreaseItemCount);
            _togggleAnimalButton.onClick.RemoveListener(ToggleAnimalState);
        }

        private void OpenInventory() => 
            _windowService.Open(WindowId.Inventory);

        private void AddItem() => 
            _inventoryService.AddItem(RandomId(), 1);

        private void DecreaseItemCount() => 
            _inventoryService.DecreaseItemCount();

        private void ToggleAnimalState() => 
            _inventoryService.ToggleAnimalState();

        private ItemId RandomId()
        {
            Array values = Enum.GetValues(typeof(ItemId));
            
            return (ItemId)values.GetValue(Random.Range(0, values.Length));
        }
    }
}