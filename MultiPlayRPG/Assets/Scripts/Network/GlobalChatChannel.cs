using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MultiPlayRPG
{
    public class GlobalChatChannel : ChatChannel
    {
        public static ChatChannel Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GlobalChatChannel::Awake: More than one instance of GlobalChat found!");
                return;
            }
            Instance = this;
        }
    }
}
