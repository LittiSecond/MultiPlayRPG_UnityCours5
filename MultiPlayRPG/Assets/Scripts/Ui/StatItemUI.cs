using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public class StatItemUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _value;
        [SerializeField] private Button _upgradeButton;

        #endregion


        #region Methods

        public void ChangeStat(int dtat)
        {
            _value.text = dtat.ToString();
        }

        public void SetUpgradable(bool isUpgradeable)
        {
            _upgradeButton.gameObject.SetActive(isUpgradeable);
        }

        #endregion
    }
}