using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class PlayerCharacterLoader : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private CharacterOfPlayerController _controller;

        [SerializeField] private PlayerScriptsConnector _scriptsConnectorr;

        //[SyncVar(hook = "HookUnitIdentity")] private NetworkIdentity _unitIdentity;

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
            //if (isServer)
            //{
            //    CharacterOfPlr character = CreateCharacter();
            //    //_controller.SetCharacter(character, true);
            //    //character.SetInventory(_inventory);
            //    //InventoryUI.Instance.SetInventory(_inventory);
            //    Inventory inventory = GetComponent<Inventory>();
            //    Equipment equipment = GetComponent<Equipment>();
            //    _scriptsConnectorr.Setup(character, inventory, equipment, true);
            //    _controller.SetCharacter(character, true);
            //}
            //else
            //{
                CmdCreatePlayer();
            //}
        }

        public override bool OnCheckObserver(NetworkConnection conn)
        {
            return false;
        }

        private void OnDestroy()
        {
            if (isServer && _scriptsConnectorr.Character != null)
            {
                UserAccount acc = AccountManager.GetAccount(connectionToClient);
                acc.Data.CharacterPos = _scriptsConnectorr.Character.transform.position;
                Destroy(_scriptsConnectorr.Character.gameObject);
                NetworkManager.singleton.StartCoroutine(acc.Quit());
            }
        }

        #endregion


        #region Methods

        [Command]
        public void CmdCreatePlayer()
        {
            CharacterOfPlr character = CreateCharacter();
            //_controller.SetCharacter(unit.GetComponent<CharacterOfPlr>(), false);
            Inventory inventory = GetComponent<Inventory>();
            Equipment equipment = GetComponent<Equipment>();
            _scriptsConnectorr.Setup(character, inventory, equipment, isLocalPlayer);
            _controller.SetCharacter(character, isLocalPlayer);
        }

        //[ClientCallback]
        //private void HookUnitIdentity(NetworkIdentity unit)
        //{
        //    if (isLocalPlayer)
        //    {
        //        _unitIdentity = unit;
        //        CharacterOfPlr character = unit.GetComponent<CharacterOfPlr>();
        //        Equipment equipment = GetComponent<Equipment>();
        //        Inventory inventory = GetComponent<Inventory>();
        //        _scriptsConnectorr.Setup(character, inventory, equipment, true);
        //        _controller.SetCharacter(character, true);
        //        //character.SetInventory(_inventory);
        //        //InventoryUI.Instance.SetInventory(_inventory);
        //    }
        //}

        private CharacterOfPlr CreateCharacter()
        {
            UserAccount acc = AccountManager.GetAccount(connectionToClient);

            GameObject unit = Instantiate(_unitPrefab, acc.Data.CharacterPos, Quaternion.identity);
            NetworkServer.Spawn(unit);
            //_unitIdentity = unit.GetComponent<NetworkIdentity>();
            TargetLinkCharacter(connectionToClient, unit.GetComponent<NetworkIdentity>());
            return unit.GetComponent<CharacterOfPlr>();
        }

        [TargetRpc]
        private void TargetLinkCharacter(NetworkConnection target, NetworkIdentity unit)
        {
            CharacterOfPlr character = unit.GetComponent<CharacterOfPlr>();
            _scriptsConnectorr.Setup(character, GetComponent<Inventory>(), GetComponent<Equipment>(), true);
            _controller.SetCharacter(character, true);
        }

        #endregion
    }
}