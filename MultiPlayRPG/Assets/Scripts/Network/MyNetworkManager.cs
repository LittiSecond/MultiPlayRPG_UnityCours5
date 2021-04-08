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

        #endregion


        #region UnityMethods

        private void Start()
        {
            if (ServerMode)
            {
                StartServer();
                NetworkServer.UnregisterHandler(MsgType.Connect);
                NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnectCustom);
                NetworkServer.RegisterHandler(MsgType.Highest + 
                    (short)NetMsgType.Login, OnServerLogin);
                NetworkServer.RegisterHandler(MsgType.Highest +
                    (short)NetMsgType.Register, OnServerRegister);
            }
        }

        #region overrided metods

        //public override void OnStartServer()
        //{
        //    Debug.Log("MyNetworkManager::OnStartServer: Server has started");
        //}

        //public override void OnStopServer()
        //{
        //    Debug.Log("MyNetworkManager::OnStopServer: Server has stopped");
        //}

        //public override void OnStartHost()
        //{
        //    Debug.Log("MyNetworkManager::OnStartHost: Host has started");
        //}

        //public override void OnStopHost()
        //{
        //    Debug.Log("MyNetworkManager::OnStopHost: Host has stopped");
        //}

        //public override void OnServerConnect(NetworkConnection conn)
        //{
        //    Debug.Log("MyNetworkManager::OnServerConnect: A client connected to the server: " + conn);
        //}

        //public override void OnServerDisconnect(NetworkConnection conn)
        //{
        //    NetworkServer.DestroyPlayersForConnection(conn);
        //    if (conn.lastError != NetworkError.Ok)
        //    {
        //        if (LogFilter.logError)
        //        {
        //            Debug.LogError("MyNetworkManager::OnServerDisconnect: ServerDisconnected due to error: " +
        //            conn.lastError);
        //        }
        //    }
        //    Debug.Log("MyNetworkManager::OnServerDisconnect: A client disconnected from the server: " + conn);
        //}

        //public override void OnServerReady(NetworkConnection conn)
        //{
        //    NetworkServer.SetClientReady(conn);
        //    Debug.Log("MyNetworkManager::OnServerReady: Client is set to the ready " +
        //        "state (ready to receive state updates): " + conn);
        //}

        //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        //{
        //    GameObject player = Instantiate(playerPrefab, new Vector3(2.0f, 0.0f, 2.0f), Quaternion.identity);
        //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //    Debug.Log("MyNetworkManager::OnServerAddPlayer: Client has requested to get his player added to the game");
        //}

        //public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
        //{
        //    if (player.gameObject != null)
        //        NetworkServer.Destroy(player.gameObject);
        //}

        //public override void OnServerError(NetworkConnection conn, int errorCode)
        //{
        //    Debug.Log("MyNetworkManager::OnServerError: Server network error occurred: " + (NetworkError)errorCode);
        //}


        //public override void OnStartClient(NetworkClient client)
        //{
        //    Debug.Log("MyNetworkManager::OnStartClient: Client has started");
        //}

        //public override void OnStopClient()
        //{
        //    Debug.Log("MyNetworkManager::OnStopClient: Client has stopped");
        //}


        //public override void OnClientConnect(NetworkConnection conn)
        //{
        //    base.OnClientConnect(conn);
        //    Debug.Log("MyNetworkManager::OnClientConnect: Connected successfully " +
        //        "to server, now to set up other stuff for the client...");
        //}

        //public override void OnClientDisconnect(NetworkConnection conn)
        //{
        //    StopClient();
        //    if (conn.lastError != NetworkError.Ok)
        //    {
        //        if (LogFilter.logError)
        //        {
        //            Debug.LogError("MyNetworkManager::OnClientDisconnect: " +
        //                "ClientDisconnected due to error: " + conn.lastError);
        //        }
        //    }
        //    Debug.Log("MyNetworkManager::OnClientDisconnect: Client disconnected from server: " + conn);
        //}

        //public override void OnClientNotReady(NetworkConnection conn)
        //{
        //    Debug.Log("MyNetworkManager::OnClientNotReady: Server has set " +
        //        "client to be not-ready (stop getting state updates)");
        //}

        //public override void OnClientError(NetworkConnection conn, int errorCode)
        //{
        //    Debug.Log("MyNetworkManager::OnClientError: Client network error occurred: " + (NetworkError)errorCode);
        //}

        //public override void OnClientSceneChanged(NetworkConnection conn)
        //{
        //    base.OnClientSceneChanged(conn);
        //    Debug.Log("MyNetworkManager::OnClientSceneChanged: Server triggered " +
        //        "scene change and we've done the same, do any extra work here for the client...");
        //}

        #endregion

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
                client.RegisterHandler(MsgType.Highest +
                    (short)NetMsgType.Login, OnClientLogin);
                client.RegisterHandler(MsgType.Highest +
                    (short)NetMsgType.Register, OnClientRegister);
            }
        }

        private IEnumerator SendLogin(string login, string pass)
        {
            while (!client.isConnected)
            {
                yield return null;
            }
            Debug.Log("MyNetworkManager::SendLogin: client login");
            client.connection.Send(MsgType.Highest + (short)NetMsgType.Login, 
                new UserMessage(login, pass));
        }

        private IEnumerator SendRegister(string login, string pass)
        {
            while (!client.isConnected)
            {
                yield return null;
            }
            Debug.Log("MyNetworkManager::SendRegister: client register");
            client.connection.Send(MsgType.Highest + (short)NetMsgType.Register,
                 new UserMessage(login, pass));
        }

        private IEnumerator LoginUser(NetworkMessage netMsg)
        {
            //UserMessage msg = netMsg.ReadMessage<UserMessage>();
            //IEnumerator e = DCF.Login(msg.Login, msg.Pass);

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
                netMsg.conn.Send(MsgType.Scene, 
                    new StringMessage(SceneManager.GetActiveScene().name));
            }
            else
            {
                Debug.Log("MyNetworkManager::LoginUser: server login fail");
                netMsg.conn.Send(MsgType.Highest + (short)NetMsgType.Login,
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
            netMsg.conn.Send(MsgType.Highest + (short)NetMsgType.Register,
                 new StringMessage(response));

        }

            #endregion


    }
}
