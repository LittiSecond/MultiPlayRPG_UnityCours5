using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public class ChatMessageUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _authorText;
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _privateButton;

        private ChatMessage _message;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetChatMessage(ChatMessage message)
        {
            _message = message;
            _authorText.text = message.Author;
            _messageText.text = message.Message;
            _privateButton.onClick.AddListener(SetPrivateMessage);
        }

        public void SetPrivateMessage()
        {
            ChatUI.Instance.SetPrivateMessage(_message);
        }

        #endregion


    }
}