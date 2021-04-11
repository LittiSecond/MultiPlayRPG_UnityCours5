using UnityEngine;
using UnityEngine.UI;

namespace MultiPlayRPG
{
    public class SkillSlotUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _holder;
        [SerializeField] private Text _timerText;

        #endregion

        #region Methods

        public void SetSkill(Skill skill)
        {
            if (skill != null)
            {
                _icon.sprite = skill.Icon;
                _holder.SetActive(false);
                _timerText.gameObject.SetActive(false);
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void SetHolder(bool active)
        {
            _holder.SetActive(active);
        }

        public void SetCastTime(float time)
        {
            _timerText.text = ((int)time).ToString();
            _timerText.gameObject.SetActive(time > 0);
        }

        #endregion
    }
}