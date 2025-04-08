using System.Collections.Generic;
using Code.Inventory;
using Code.Services.StaticDataService;
using Code.StaticData;
using Code.UI.Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public class InventoryWindow : BaseWindow
    {
        [SerializeField] private Button CloseButton;
        [SerializeField] private Transform ScrollView;

        private IWindowService _windowService;
        private IInventoryService _inventoryService;
        
        private InventoryCellView[] _cells = {};
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IWindowService windowService, IInventoryService inventoryService, IStaticDataService staticDataService)
        {
            Id = WindowId.Inventory;
            
            _staticDataService = staticDataService;
            _windowService = windowService;
            _inventoryService = inventoryService;
        }

        protected override void Initialize()
        {
            CloseButton.onClick.AddListener(Close);
            InitializeCells(ScrollView);
            UpdateCells();
            
            _inventoryService.OnInventoryChanged += UpdateCells;
        }
        

        protected override void Cleanup()
        {
            base.Cleanup();
            CloseButton.onClick.RemoveListener(Close);
            _inventoryService.OnInventoryChanged -= UpdateCells;
            
            _cells = null;
        }

        private void Close()
        {
            _windowService.Close(Id);
        }

        private void InitializeCells(Transform parent)
        {
            _cells = new InventoryCellView[_staticDataService.GetInventoryConfig().CellCount];
            
            for (int i = 0; i < _cells.Length; i++)
            {
                GameObject cellObj = Instantiate(_staticDataService.GetInventoryConfig().CellPrefab, parent);
                _cells[i] = cellObj.GetComponent<InventoryCellView>();
            }
        }

        private void UpdateCells()
        {
            List<InventoryItem> items = _inventoryService.GetItems();
            
            for (int i = 0; i < _cells.Length; i++)
            {
                if (i < items.Count)
                {
                    InventoryItem item = items[i];
                    ItemDefinition itemDef = _staticDataService.GetItemDefinition(item.ID);
                    _cells[i].SetData(item, itemDef);
                }
                else
                {
                    _cells[i].Clear();
                }
            }
        }
    }
}