using UnityEngine;


namespace MultiPlayRPG
{
    public class SkillsPanelUI : MonoBehaviour
    {

        #region Fields

        [SerializeField] private SkillSlotUI[] _slots;

        public static SkillsPanelUI Instance;
        
        private UnitSkills _skills;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("SkillsPanelUI::Awake: More than one instance of SkillsPanel found!");
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (_skills != null)
            {
                bool inCast = _skills.InCast;
                for (int i = 0; i < _skills.Count && i < _slots.Length; i++)
                {
                    _slots[i].SetCastTime(_skills[i].CastDelay);

                    _slots[i].SetHolder(
                        inCast || _skills[i].CastDelay > 0
                        || _skills[i].CooldownDelay > 0
                    );
                }
            }
        }

        #endregion


        #region Methods

        public void SetSkills(UnitSkills skills)
        {
            _skills = skills;
            int length = (_slots.Length <= _skills.Count) ? _slots.Length : _skills.Count;
            for (int i = 0; i < length; i++)
            {
                _slots[i].SetSkill(_skills[i]);
            }
        }

        #endregion
    }
}