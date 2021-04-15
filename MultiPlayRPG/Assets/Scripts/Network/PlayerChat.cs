using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class PlayerChat : NetworkBehaviour
    {
        #region Fields

        public delegate void ChangeChannelsDelegate();
        public event ChangeChannelsDelegate OnChangeChannels;
        public delegate void ReciveMessageDelegate(ChatMessage message);
        public event ReciveMessageDelegate OnReciveMessage;

        public static PlayerChat Instance;
        public List<ChatChannel> Channels = new List<ChatChannel>();

        #endregion


        #region Properties

        #endregion


        #region UnityMethods

        public override void OnStartClient()
        {
            if (Instance != null)
            {
                Debug.LogError("PlayerChat::OnStartClient: More than one instance of PlayerChat found!");
                return;
            }
            Instance = this;
        }

        #endregion


        #region Methods

        public void RegisterChannel(ChatChannel channel)
        {
            Channels.Add(channel);
            if (OnChangeChannels != null)
            {
                OnChangeChannels.Invoke();
            }
        }

        [Command]
        public void CmdSendFromChannel(GameObject channelGO, ChatMessage message)
        {
            message.Author = AccountManager.GetAccount(connectionToClient).Login;
            channelGO.GetComponent<ChatChannel>().SendFromChanel(message);
        }

        public void ReciveChatMessage(ChatMessage message)
        {
            OnReciveMessage.Invoke(message);
        }

        #endregion

    }
}
