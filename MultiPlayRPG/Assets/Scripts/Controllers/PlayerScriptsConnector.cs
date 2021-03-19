using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(StatsManager), typeof(NetworkIdentity), 
        typeof(CharacterProgress))]
    public class PlayerScriptsConnector : MonoBehaviour
    {

        #region Fields

        [SerializeField] private CharacterOfPlr _character;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Equipment _equipment;
        [SerializeField] private StatsManager _statsManager;
        [SerializeField] private CharacterProgress _progress;

        #endregion


        #region Properties

        public CharacterOfPlr Character {  get { return _character; } }
        public Inventory Inventoryy { get { return _inventory; } }
        public Equipment Equipmentt { get { return _equipment; } }
        public CharacterProgress Progress { get { return _progress; } }
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
            _statsManager = GetComponent<StatsManager>();
            _progress = GetComponent<CharacterProgress>();
            _statsManager.Player = this;

            if (isLocalPlayer)
            {
                InventoryUI.Instance.SetInventory(_inventory);
                EquipmentUI.Instance.SetEquipment(_equipment);
                StatsUI.Instance.SetManager(_statsManager);
            }

            if (GetComponent<NetworkIdentity>().isServer)
            {
                _character.Stats.Manager = _statsManager;
                _progress.Manager = _statsManager;
            }
        }

        #endregion

    }
}
