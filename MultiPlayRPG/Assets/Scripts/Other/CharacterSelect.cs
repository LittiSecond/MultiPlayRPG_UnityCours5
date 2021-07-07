using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class CharacterSelect : MonoBehaviour
    {
        #region Fields

        [SerializeField] private MyNetworkManager _netManager;

        public static CharacterSelect Instance;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _netManager.serverRegisterHandler += RegisterServerHandler;
                _netManager.clientRegisterHandler += RegisterClientHandler;
            }
        }


        #endregion


        #region Methods

        private void RegisterServerHandler()
        {
            NetworkServer.RegisterHandler(MsgType.Highest + 1 + 
                (short)NetMsgType.SelectCharacter, OnSelectCharacter);
        }

        private void RegisterClientHandler(NetworkClient client)
        {
            client.RegisterHandler(MsgType.Highest + 1 + 
                (short)NetMsgType.SelectCharacter, OnOpenSelectUI);
        }

        private void OnSelectCharacter(NetworkMessage netMsg)
        {
            NetworkHash128 hash = netMsg.reader.ReadNetworkHash128();
            if (hash.IsValid())
            {
                UserAccount account = AccountManager.GetAccount(netMsg.conn);
                account.Data.CharacterHash = hash;
                _netManager.AccountEnter(account);
            }
        }

        private void OnOpenSelectUI(NetworkMessage netMsg)
        {
            //Debug.Log("CharacterSelect::OnOpenSelectUI: CharacterSelectUI.Instance.OpenPanel();");
            CharacterSelectUI.Instance.OpenPanel();
        }

        public void SelectCharacter(NetworkHash128 characterHash)
        {
            if (characterHash.IsValid())
            {
                _netManager.client.Send(MsgType.Highest + 1 + 
                    (short)NetMsgType.SelectCharacter, new HashMessage(characterHash));
            }
        }

        #endregion

    }
}