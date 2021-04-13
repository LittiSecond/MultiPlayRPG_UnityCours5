using System;
using UnityEngine;


namespace MultiPlayRPG
{
    [Serializable]
    public class UnitSkills
    {
        #region Fields

        [SerializeField] private Skill[] _skills;

        private UserData _data;

        #endregion


        #region Properties

        public int Count 
        {
            get
            {
                return _skills.Length;
            }
        }

        public Skill this[int index]
        {
            get
            {
                return _skills[index];
            }
            set
            {
                _skills[index] = value;
            }
        }

        public bool InCast
        {
            get
            {
                for (int i = 0; i < _skills.Length; i++)
                {
                    if (_skills[i].CastDelay > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void Load(UserData data)
        {
            _data = data;
            for (int i = 0; i < _skills.Length; i++)
            {
                UpgradableSkill skill = _skills[i] as UpgradableSkill;
                if (i >= _data.Skills.Count)
                {
                    _data.Skills.Add(skill.Level);
                }
                else
                {
                    skill.Level = _data.Skills[i];
                }
                skill.OnSetLevel += ChangeLevel;
            }
        }

        void ChangeLevel(UpgradableSkill skill, int newLevel)
        {
            for (int i = 0; i < _skills.Length; i++)
            {
                if (_skills[i] == skill)
                {
                    _data.Skills[i] = newLevel;
                    break;
                }
            }
        }

        #endregion
    }
}
