using Code.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Inventory
{
    public class InventoryCellView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text countText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text woundedIndicator;

        public void SetData(InventoryItem item, ItemDefinition itemDef)
        {
            iconImage.sprite = itemDef.Icon;
            iconImage.enabled = true;
            countText.text = item.Count.ToString();
            nameText.text = itemDef.Name;
            nameText.enabled = true;
            countText.enabled = true;
            
            UpdateWoundedIndicator(itemDef.Type, item.State);
        }
        
        public void Clear()
        {
            iconImage.enabled = false;
            nameText.enabled = false;
            countText.enabled = false;
            woundedIndicator.text = " ";
        }

        private void UpdateWoundedIndicator(ItemType type, AnimalState? state)
        {
            if (type != ItemType.Animal)
            {
                woundedIndicator.text = " ";
                return;
            }

            woundedIndicator.text = state switch
            {
                AnimalState.Wounded => "Ранен",
                AnimalState.Healthy => "Целый",
                _ => " "
            };
        }
    }
}