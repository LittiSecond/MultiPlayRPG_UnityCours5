using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace MultiPlayRPG
{
    public class UserMessage : MessageBase
    {
        #region Fields

        public string Login;
        public string Pass;

        #endregion


        #region ClassLifeCycles

        public UserMessage()
        {

        }

        public UserMessage(string login, string pass)
        {
            Login = login;
            Pass = pass;
        }

        #endregion


        #region Methods

        public override void Deserialize(NetworkReader reader)
        {
            Login = reader.ReadString();
            Pass = reader.ReadString();
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(Login);
            writer.Write(Pass);
        }

        #endregion

    }
}
