using System;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    [Serializable]
    public class ChatMessage
    {
        #region Fields

        public NetworkInstanceId SenderId;
        public NetworkInstanceId ReciverId;
        public string Author;
        public string Message;

        #endregion


        #region ClassLifeCycles

        public ChatMessage()
        { }

        public ChatMessage(NetworkInstanceId sender, NetworkInstanceId reciver, string author, string message)
        {
            SenderId = sender;
            ReciverId = reciver;
            Author = author;
            Message = message;
        }

        #endregion
    }
}
