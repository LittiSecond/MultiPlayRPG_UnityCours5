using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using DatabaseControl;


namespace MultiPlayRPG
{
    public class MyNetworkManager: NetworkManager
    {

        #region Fields

        public bool ServerMode;

        public delegate void ResponseDelegate(string response);
        public ResponseDelegate loginResponseDelegate;
        public ResponseDelegate registerResponseDelegate;

        public delegate void RegisterServerHandlerDelegate();
        public RegisterServerHandlerDelegate serverRegisterHandler;
        public delegate void RegisterClientHandlerDelegate(NetworkClient client);
        public RegisterClientHandlerDelegate clientRegisterHandler;

        #endregion


        #region UnityMethods

        private void Start()
        {
            if (ServerMode)
            {
                StartServer();
                NetworkServer.UnregisterHandler(MsgType.Connect);
                NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnectCustom);
                NetworkServer.RegisterHandler(MsgType.Highest + 1 +
                    (short)NetMsgType.Login, OnServerLogin);
                NetworkServer.RegisterHandler(MsgType.Highest + 1 +
                    (short)NetMsgType.Register, OnServerRegister);
                serverRegisterHandler?.Invoke();
            }
        }

        #endregion


        #region Methods

        public void Login(string login, string pass)
        {
            ClientConnect();
            StartCoroutine(SendLogin(login, pass));
        }

        public void Register(string login, string pass)
        {
            ClientConnect();
            StartCoroutine(SendRegister(login, pass));
        }

        private void OnServerLogin(NetworkMessage netMsg)
        {
            StartCoroutine(LoginUser(netMsg));
        }

        private void OnServerRegister(NetworkMessage netMsg)
        {
            StartCoroutine(RegisterUser(netMsg));
        }

        void OnClientLogin(NetworkMessage netMsg)
        {
            loginResponseDelegate.Invoke(netMsg.reader.ReadString());
        }

        void OnClientRegister(NetworkMessage netMsg)
        {
            registerResponseDelegate.Invoke(netMsg.reader.ReadString());
        }

        private void OnServerConnectCustom(NetworkMessage netMsg)
        {
            if (LogFilter.logDebug)
            {
                Debug.Log("MyNetworkManager::OnServerConnectCustom: ");
            }
            netMsg.conn.SetMaxDelay(maxDelay);
            OnServerConnect(netMsg.conn);
        }

        private void ClientConnect()
        {
            NetworkClient client = this.client;
            if (client == null)
            {
                client = StartClient();
                client.RegisterHandler(MsgType.Highest + 1 +
                    (short)NetMsgType.Login, OnClientLogin);
                client.RegisterHandler(MsgType.Highest + 1 +
                    (short)NetMsgType.Register, OnClientRegister);
                clientRegisterHandler?.Invoke(client);
            }
        }

        private IEnumerator SendLogin(string login, string pass)
        {
            while (!client.isConnected)
            {
                yield return null;
            }
            Debug.Log("MyNetworkManager::SendLogin: client login");
            client.connection.Send(MsgType.Highest + 1 + (short)NetMsgType.Login, 
                new UserMessage(login, pass));
        }

        private IEnumerator SendRegister(string login, string pass)
        {
            while (!client.isConnected)
            {
                yield return null;
            }
            Debug.Log("MyNetworkManager::SendRegister: client register");
            client.connection.Send(MsgType.Highest + 1 + (short)NetMsgType.Register,
                 new UserMessage(login, pass));
        }

        private IEnumerator LoginUser(NetworkMessage netMsg)
        {
            UserAccount account = new UserAccount(netMsg.conn);
            UserMessage msg = netMsg.ReadMessage<UserMessage>();
            IEnumerator e = account.LoginTo(msg.Login, msg.Pass);

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            string response = e.Current as string;

            if (response == "Success")
            {
                Debug.Log("MyNetworkManager::LoginUser: server login success");

                if (account.Data.CharacterHash.IsValid())
                {
                    AccountEnter(account);
                }
                else
                {
                    netMsg.conn.Send(MsgType.Highest + 1 + 
                        (short)NetMsgType.Login, new StringMessage("CharacterNotSelect"));
                    netMsg.conn.Send(MsgType.Highest + 1 + 
                        (short)NetMsgType.SelectCharacter, new EmptyMessage());
                }
            }
            else
            {
                Debug.Log("MyNetworkManager::LoginUser: server login fail");
                netMsg.conn.Send(MsgType.Highest + 1 + (short)NetMsgType.Login,
                     new StringMessage(response));
            }
        }

        IEnumerator RegisterUser(NetworkMessage netMsg)
        {
            UserMessage msg = netMsg.ReadMessage<UserMessage>();
            IEnumerator e = DCF.RegisterUser(msg.Login, msg.Pass, "");

            while (e.MoveNext())
            {
                yield return e.Current;
            }
            string response = e.Current as string;

            Debug.Log("MyNetworkManager::RegisterUser: server register done");
            netMsg.conn.Send(MsgType.Highest + 1 + (short)NetMsgType.Register,
                 new StringMessage(response));

        }

        public void AccountEnter(UserAccount account)
        {
            account.Connection.Send(MsgType.Scene, new StringMessage(onlineScene));
        }

        #endregion


    }
}
