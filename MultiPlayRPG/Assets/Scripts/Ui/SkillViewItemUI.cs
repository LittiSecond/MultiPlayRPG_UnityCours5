using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public class SkillViewItemUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _icon;
        [SerializeField] private Text _levelText;
        [SerializeField] private GameObject _holder;

        #endregion


        #region Methods

        public void SetSkill(UpgradableSkill skill)
        {
            if (skill != null)
            {
                _icon.sprite = skill.Icon;
                skill.OnSetLevel += ChangeLevel;
                ChangeLevel(skill, skill.Level);
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void ChangeLevel(UpgradableSkill skill, int newLevel)
        {
            _levelText.text = newLevel.ToString();
        }

        public void SetUpgradable(bool active)
        {
            _holder.SetActive(active);
        }

        #endregion
    }
}