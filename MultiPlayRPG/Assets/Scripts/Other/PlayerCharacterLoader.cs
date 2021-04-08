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

        #endregion


        #region UnityMethods

        public override void OnStartAuthority()
        {
                CmdCreatePlayer();
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
            Inventory inventory = GetComponent<Inventory>();
            Equipment equipment = GetComponent<Equipment>();
            _scriptsConnectorr.Setup(character, inventory, equipment, isLocalPlayer);
            _controller.SetCharacter(character, isLocalPlayer);
        }

        private CharacterOfPlr CreateCharacter()
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(unit);
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