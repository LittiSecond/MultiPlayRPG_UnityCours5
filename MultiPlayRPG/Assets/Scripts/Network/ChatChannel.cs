using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class ChatChannel : NetworkBehaviour
    {
        #region Fields

        public string ChannelName;

        #endregion


        #region Methods
        public virtual void SendFromChanel(ChatMessage message)
        {
            RpcSendFromChanel(message);
        }

        [ClientRpc]
        protected void RpcSendFromChanel(ChatMessage message)
        {
            PlayerChat.Instance.ReciveChatMessage(message);
        }

        public virtual void SendFromPlayerChat(string text)
        {
            ChatMessage msg = new ChatMessage(PlayerChat.Instance.netId, 
                NetworkInstanceId.Invalid, PlayerChat.Instance.name, text);
            PlayerChat.Instance.CmdSendFromChannel(gameObject, msg);
        }
        #endregion

    }
}