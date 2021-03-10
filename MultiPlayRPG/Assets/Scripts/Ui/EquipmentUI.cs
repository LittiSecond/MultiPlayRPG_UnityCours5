using UnityEngine;
using System.Collections;

namespace MultiPlayRPG
{
    public sealed class EquipmentUI : MonoBehaviour
    {
        #region Fields

        public static EquipmentUI Instance;

        [SerializeField] private GameObject _equipmentUI;

        [SerializeField] private EquipmentSlotUI _headSlot;
        [SerializeField] private EquipmentSlotUI _chestSlot;
        [SerializeField] private EquipmentSlotUI _legsSlot;
        [SerializeField] private EquipmentSlotUI _rigthSlot;
        [SerializeField] private EquipmentSlotUI _leftSlot;

        private EquipmentSlotUI[] _slots;
        private Equipment _equipment;

        #endregion


        #region Properties

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of EquipmentUI found!");
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            _slots = new EquipmentSlotUI[ System.Enum.GetValues(typeof(EquipmentSlotType)).Length];
            _slots[(int)EquipmentSlotType.Head] = _headSlot;
            _slots[(int)EquipmentSlotType.Chest] = _chestSlot;
            _slots[(int)EquipmentSlotType.LeftHand] = _leftSlot;
            _slots[(int)EquipmentSlotType.RightHand] = _rigthSlot;
            _slots[(int)EquipmentSlotType.Legs] = _legsSlot;

            _equipmentUI.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Equipment"))
            {
                _equipmentUI.SetActive(!_equipmentUI.activeSelf);
            }
        }

        #endregion


        #region Methods

        public void SetEquipment(Equipment newEquipment)
        {
            _equipment = newEquipment;
            _equipment.OnItemChanged += ItemChanged;
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] != null)
                {
                    _slots[i].Equipmentt = _equipment;
                }
            }
            ItemChanged(0, 0);
        }

        private void ItemChanged( UnityEngine.Networking.SyncList<Item>.Operation op,
            int itemIndex)
        {
            for (int i = 1; i < _slots.Length; i++)
            {
                _slots[i].ClearSlot();
            }
            for (int i = 0; i < _equipment.Items.Count; i++)
            {
                int slotIndex = (int)((EquipmentItem)_equipment.Items[i]).EquipmentSlot;
                _slots[slotIndex].SetItem(_equipment.Items[i]);
            }
        }



        #endregion




    }
}