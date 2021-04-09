using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class LoginIUUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _currentPanel;
        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private GameObject _registerPanel;
        [SerializeField] private GameObject _loadingPanel;

        [SerializeField] private InputField _loginLogin;
        [SerializeField] private InputField _loginPass;
        [SerializeField] private InputField _registrLogin;
        [SerializeField] private InputField _registrPass;
        [SerializeField] private InputField _registrConfirm;

        private MyNetworkManager _netManager;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _netManager = NetworkManager.singleton as MyNetworkManager;
            if (_netManager.ServerMode )
            {
                _loginPanel.SetActive(false);
            }
            else
            {
                _netManager.loginResponseDelegate = LoginResponse;
                _netManager.registerResponseDelegate = RegisterResponse;
            }
        }

        #endregion


        #region Methods

        public void Login()
        {
            //NetworkManager.singleton.StartClient();
            _netManager.Login(_loginLogin.text, _loginPass.text);
            _currentPanel.SetActive(false);
            _loadingPanel.SetActive(true);
        }

        private void ClearInputs()
        {
            _loginLogin.text = string.Empty;
            _loginPass.text = string.Empty;
            _registrLogin.text = string.Empty;
            _registrPass.text = string.Empty;
            _registrConfirm.text = string.Empty;
        }

        public void SetPenel(GameObject panel)
        {
            _currentPanel.SetActive(false);
            _currentPanel = panel;
            _currentPanel.SetActive(true);
            ClearInputs();
        }

        public void Register()
        {
            if (_registrPass.text != string.Empty && _registrPass.text == _registrConfirm.text)
            {
                _netManager.Register(_registrLogin.text, _registrPass.text);
                _currentPanel.SetActive(false);
                _loadingPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Error: Password Incorrect");
                ClearInputs(); 
            }
        }

        public void LoginResponse(string response)
        {
            switch (response)
            {
                case "UserError":
                    Debug.Log("Error: Username not Found");
                    break;
                case "PassError":
                    Debug.Log("Error: Password Incorrect");
                    break;
                default:
                    Debug.Log("Error: Unknown Error. Please try again later.");
                    break;
            }
            Debug.Log("LoginIUUI::LoginResponse: response = " + response);

            _loadingPanel.SetActive(false);
            _currentPanel.SetActive(true);
            ClearInputs();
        }

        public void RegisterResponse(string response)
        {
            switch (response)
            {
                case "Success":
                    Debug.Log("User registered");
                    break;
                case "UserError":
                    Debug.Log("Error: Username Already Taken");
                    break;
                default:
                    Debug.Log("Error: Unknown Error. Please try again later.");
                    break;
            }
            Debug.Log("LoginIUUI::RegisterResponse: response = " + response);

            _loadingPanel.SetActive(false);
            _currentPanel.SetActive(true);
            ClearInputs();
        }


        #endregion
    }
}