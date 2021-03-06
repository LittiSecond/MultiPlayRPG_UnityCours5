using UnityEngine;
using System.Collections;

namespace MultiPlayRPG
{
    public class InventoryUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _inventoryUI;
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private InventorySlotUI _slotPrefab;

        public static InventoryUI Instance;

        private InventorySlotUI[] _slots;
        private Inventory _inventory;

        #endregion


        #region Properties

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _inventoryUI.SetActive(false);
            if (Instance != null)
            {
                Debug.LogError("More than one instance of InventoryUI found!");
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                _inventoryUI.SetActive(!_inventoryUI.activeSelf);
            }
        }

        #endregion


        #region Methods

        public void SetInventory(Inventory newInwentory)
        {
            _inventory = newInwentory;
            _inventory.OnItemChanged += ItemChanged;

            InventorySlotUI[] children = _itemsParent.GetComponentsInChildren<InventorySlotUI>();

            for (int i = 0; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }

            _slots = new InventorySlotUI[_inventory.Space];
            for (int i = 0; i < _inventory.Space; i++)
            {
                _slots[i] = Instantiate(_slotPrefab, _itemsParent);
                _slots[i].Inventoryy = _inventory;
                if (i < _inventory.Items.Count)
                {
                    _slots[i].SetItem(_inventory.Items[i]);
                }
                else
                {
                    _slots[i].ClearSlot(); 
                }
            }
        }

        private void ItemChanged(UnityEngine.Networking.SyncList<Item>.Operation op, int itemIndex)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (i < _inventory.Items.Count)
                {
                    _slots[i].SetItem(_inventory.Items[i]);
                }
                else
                {
                    _slots[i].ClearSlot();
                }
            }

        }

        #endregion
    }
}