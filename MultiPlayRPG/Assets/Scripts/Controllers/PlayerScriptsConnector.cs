using UnityEngine;


namespace MultiPlayRPG
{
    public class PlayerScriptsConnector : MonoBehaviour
    {

        #region Fields

        [SerializeField] private CharacterOfPlr _character;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Equipment _equipment;

        #endregion


        #region Properties

        public CharacterOfPlr Character {  get { return _character; } }
        public Inventory Inventoryy { get { return _inventory; } }
        public Equipment Equipmentt { get { return _equipment; } }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void Setup(CharacterOfPlr character, Inventory inventory, Equipment equipment, bool isLocalPlayer)
        {
            _character = character;
            _inventory = inventory;
            _equipment = equipment;
            _character.PlayerScriptsConnectorr = this;
            _inventory.PlayerScriptsConnectorr = this;
            _equipment.PlayerScriptsConnectorr = this;

            if (isLocalPlayer)
            {
                InventoryUI.Instance.SetInventory(_inventory);
                EquipmentUI.Instance.SetEquipment(_equipment);
            }
        }

        #endregion

    }
}
