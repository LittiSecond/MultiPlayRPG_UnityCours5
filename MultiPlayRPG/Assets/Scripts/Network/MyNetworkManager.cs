using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class MyNetworkManager: NetworkManager
    {

        #region Fields

        public bool _isServerMode;

        #endregion


        #region UnityMethods

        private void Start()
        {
            if (_isServerMode)
            {
                StartServer();
            }
        }


        public override void OnStartServer()
        {
            Debug.Log("MyNetworkManager::OnStartServer: Server has started");
        }

        public override void OnStopServer()
        {
            Debug.Log("MyNetworkManager::OnStopServer: Server has stopped");
        }

        public override void OnStartHost()
        {
            Debug.Log("MyNetworkManager::OnStartHost: Host has started");
        }

        public override void OnStopHost()
        {
            Debug.Log("MyNetworkManager::OnStopHost: Host has stopped");
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            Debug.Log("MyNetworkManager::OnServerConnect: A client connected to the server: " + conn);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            NetworkServer.DestroyPlayersForConnection(conn);
            if (conn.lastError != NetworkError.Ok)
            {
                if (LogFilter.logError)
                {
                    Debug.LogError("MyNetworkManager::OnServerDisconnect: ServerDisconnected due to error: " +
                    conn.lastError);
                }
            }
            Debug.Log("MyNetworkManager::OnServerDisconnect: A client disconnected from the server: " + conn);
        }

        public override void OnServerReady(NetworkConnection conn)
        {
            NetworkServer.SetClientReady(conn);
            Debug.Log("MyNetworkManager::OnServerReady: Client is set to the ready " +
                "state (ready to receive state updates): " + conn);
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(2.0f, 0.0f, 2.0f), Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            Debug.Log("MyNetworkManager::OnServerAddPlayer: Client has requested to get his player added to the game");
        }

        public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
        {
            if (player.gameObject != null)
                NetworkServer.Destroy(player.gameObject);
        }

        public override void OnServerError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("MyNetworkManager::OnServerError: Server network error occurred: " + (NetworkError)errorCode);
        }


        public override void OnStartClient(NetworkClient client)
        {
            Debug.Log("MyNetworkManager::OnStartClient: Client has started");
        }

        public override void OnStopClient()
        {
            Debug.Log("MyNetworkManager::OnStopClient: Client has stopped");
        }


        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            Debug.Log("MyNetworkManager::OnClientConnect: Connected successfully " +
                "to server, now to set up other stuff for the client...");
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            StopClient();
            if (conn.lastError != NetworkError.Ok)
            {
                if (LogFilter.logError)
                {
                    Debug.LogError("MyNetworkManager::OnClientDisconnect: " +
                        "ClientDisconnected due to error: " + conn.lastError);
                }
            }
            Debug.Log("MyNetworkManager::OnClientDisconnect: Client disconnected from server: " + conn);
        }

        public override void OnClientNotReady(NetworkConnection conn)
        {
            Debug.Log("MyNetworkManager::OnClientNotReady: Server has set " +
                "client to be not-ready (stop getting state updates)");
        }

        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("MyNetworkManager::OnClientError: Client network error occurred: " + (NetworkError)errorCode);
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            base.OnClientSceneChanged(conn);
            Debug.Log("MyNetworkManager::OnClientSceneChanged: Server triggered " +
                "scene change and we've done the same, do any extra work here for the client...");
        }

        #endregion

    }
}
