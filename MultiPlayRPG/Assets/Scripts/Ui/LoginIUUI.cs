using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class LoginIUUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _loginPanel;

        #endregion


        #region UnityMethods

        private void Start()
        {
            if ( (NetworkManager.singleton as MyNetworkManager).ServerMode )
            {
                _loginPanel.SetActive(false);
            }
        }

        #endregion


        #region Methods

        public void Login()
        {
            NetworkManager.singleton.StartClient();
        }

        #endregion
    }
}