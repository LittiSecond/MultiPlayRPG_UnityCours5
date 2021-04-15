using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class PrivateChatChannel : ChatChannel
    {
        #region Fields

        private NetworkInstanceId _reciver;

        #endregion


        #region Methods

        public void SetReciverMessage(ChatMessage message)
        {
            _reciver = message.SenderId;
            ChannelName = message.Author;
        }

        public override void SendFromPlayerChat(string text)
        {
            ChatMessage msg = new ChatMessage(PlayerChat.Instance.netId, _reciver, 
                PlayerChat.Instance.name, text);
            PlayerChat.Instance.CmdSendFromChannel(gameObject, msg);
        }

        public override void SendFromChanel(ChatMessage message)
        {
            TargetSendFromChanel(
                 NetworkServer.objects[message.ReciverId].connectionToClient, message);
        }

        [TargetRpc]
        protected void TargetSendFromChanel(NetworkConnection target, ChatMessage message)
        {
            PlayerChat.Instance.ReciveChatMessage(message);
        }

        #endregion

    }
}
