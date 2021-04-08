using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class CharacterSelectUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private GameObject _selectPanel;

        public static CharacterSelectUI Instance;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("CharacterSelectUI::Awake: " +
                    "More than one instance of CharacterSelectUI found!");
            }
        }

        #endregion


        #region Methods

        public void OpenPanel()
        {
            _loginPanel.SetActive(false);
            _selectPanel.SetActive(true);
        }

        public void SelectCharacter(NetworkIdentity characterIdentity)
        {
            CharacterSelect.Instance.SelectCharacter(characterIdentity.assetId);
        }

        #endregion

    }
}