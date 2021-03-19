using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public sealed class EquipmentSlotUI : MonoBehaviour
    {
        #region Fields

        public Image Icon;
        public Button UnEquipButton;
        public Equipment Equipmentt;

        private Item _item;

        #endregion


        #region UnityMethods

        private void Start()
        {
            if (UnEquipButton != null)
            {
                UnEquipButton.onClick.AddListener(UnEquip);
            }
        }

        #endregion


        #region Methods

        public void SetItem(Item newItem)
        {
            _item = newItem;
            Icon.sprite = _item.Icon;
            Icon.enabled = true;
            UnEquipButton.interactable = true;
        }

        public void ClearSlot()
        {
            _item = null;
            Icon.sprite = null;
            Icon.enabled = false;
            UnEquipButton.interactable = false;
        }

        public void UnEquip()
        {
            Equipmentt.UnEquipItem(_item);
        }

        #endregion


    }
}