using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class PlayerCharacterLoader : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private CharacterOfPlayerController _controller;

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
                GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
                NetworkServer.Spawn(unit);
                _unitIdentity = unit.GetComponent<NetworkIdentity>();
                _controller.SetCharacter(unit.GetComponent<CharacterOfPlr>(), true);
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
                GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
                NetworkServer.Spawn(unit);
                _unitIdentity = unit.GetComponent<NetworkIdentity>();
                _controller.SetCharacter(unit.GetComponent<CharacterOfPlr>(), false);
        }

        [ClientCallback]
        void HookUnitIdentity(NetworkIdentity unit)
        {
            if (isLocalPlayer)
            {
                _unitIdentity = unit;
                _controller.SetCharacter(unit.GetComponent<CharacterOfPlr>(), true);
            }
        }

        #endregion
    }
}