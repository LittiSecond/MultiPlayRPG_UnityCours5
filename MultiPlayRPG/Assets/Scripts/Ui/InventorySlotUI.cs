using UnityEngine;
using UnityEngine.UI;

namespace MultiPlayRPG
{
    public sealed class InventorySlotUI : MonoBehaviour
    {
        #region Fields

        public Image Icon;
        public Button RemoveButton;
        public Inventory Inventoryy;

        private Item _item;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetItem(Item newItem)
        {
            _item = newItem;
            Icon.sprite = _item.Icon;
            Icon.enabled = true;
            RemoveButton.interactable = true;
        }

        public void ClearSlot()
        {
            _item = null;
            Icon.sprite = null;
            Icon.enabled = false;
            RemoveButton.interactable = false;
        }

        public void OnRemoveButton()
        {
            Inventoryy.Remove(_item);
        }

        public void UseItem()
        {
            if (_item != null)
            {
                _item.Use();
            }
        }

        #endregion
    }
}