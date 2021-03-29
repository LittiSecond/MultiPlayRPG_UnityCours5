using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class MenuUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _menuPanel;

        #endregion


        #region UnityMethods

        private void Start()
        {
            if ((NetworkManager.singleton as MyNetworkManager).ServerMode)
            {
                _menuPanel.SetActive(false);
            }
        }

        #endregion


        #region Methods

        public void Disconnect()
        {
            if (NetworkManager.singleton.IsClientConnected())
            { 
                NetworkManager.singleton.StopClient(); 
            }
        }

        #endregion



    }
}