using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class PlayerCharacterLoader : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private CharacterOfPlayerController _controller;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerScriptsConnector _scriptsConnectorr;

        [SyncVar(hook = "HookUnitIdentity")] private NetworkIdentity _unitIdentity;

        #endregion


        #region UnityMethods

        //public override void OnStartServer()
        //{
        //    GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
        //    NetworkServer.SpawnWithClientAuthority(unit, gameObject);
        //}

        public override void OnStartAuthority()
        {
            //base.OnStartAuthority();
            if (isServer)
            {
                CharacterOfPlr character = CreateCharacter();
                //_controller.SetCharacter(character, true);
                //character.SetInventory(_inventory);
                //InventoryUI.Instance.SetInventory(_inventory);
                _inventory = GetComponent<Inventory>();
                Equipment equipment = GetComponent<Equipment>();
                _scriptsConnectorr.Setup(character, _inventory, equipment, true);
            }
            else
            {
                CmdCreatePlayer();
            }
        }

        public override bool OnCheckObserver(NetworkConnection conn)
        {
            return false;
        }


        #endregion


        #region Methods

        [Command]
        public void CmdCreatePlayer()
        {
            CharacterOfPlr character = CreateCharacter();
            //_controller.SetCharacter(unit.GetComponent<CharacterOfPlr>(), false);
            _inventory = GetComponent<Inventory>();
            Equipment equipment = GetComponent<Equipment>();
            _scriptsConnectorr.Setup(character, _inventory, equipment, false);
        }

        [ClientCallback]
        private void HookUnitIdentity(NetworkIdentity unit)
        {
            if (isLocalPlayer)
            {
                _unitIdentity = unit;
                CharacterOfPlr character = unit.GetComponent<CharacterOfPlr>();
                Equipment equipment = GetComponent<Equipment>();
                _inventory = GetComponent<Inventory>();
                _scriptsConnectorr.Setup(character, _inventory, equipment, true);
                _controller.SetCharacter(character, true);
                //character.SetInventory(_inventory);
                //InventoryUI.Instance.SetInventory(_inventory);
            }
        }

        private CharacterOfPlr CreateCharacter()
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(unit);
            _unitIdentity = unit.GetComponent<NetworkIdentity>();
            return unit.GetComponent<CharacterOfPlr>();
        }

        #endregion
    }
}