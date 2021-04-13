using System;
using UnityEngine;

namespace MultiPlayRPG
{
    public class CharacterProgress : MonoBehaviour
    {
        #region Fields

        private const float EXPERIENCE_STEP = 100.0f;
        private const int STAT_POINTS_PER_LEVEL = 3;
        private const int SKILL_POINTS_PER_LEVEL = 1;

        private StatsManager _manager;
        private UserData _data;

        private float _expa;
        private float _nextLevelExp;
        private int _level;
        private int _statPoints;
        private int _skillPoints;

        #endregion


        #region Properties

        public StatsManager Manager
        {
            set
            {
                _manager = value;
                _manager.Expa = _expa;
                _manager.NextLevelExp = _nextLevelExp;
                _manager.Level = _level;
                _manager.StatPoints = _statPoints;
                _manager.SkillPoints = _skillPoints;
            }
        }

        #endregion


        #region Methods

        public void AddExp(float addExpa)
        {
            _data.Expa =  _expa += addExpa;
            while (_expa >= _nextLevelExp)
            {
                _data.Expa = _expa -= _nextLevelExp;
                LevelUp();
            }
            if (_manager != null)
            {
                _manager.Expa = _expa;
                _manager.NextLevelExp = _nextLevelExp;
                _manager.Level = _level;
                _manager.StatPoints = _statPoints;
                _manager.SkillPoints = _skillPoints;
            }
        }

        private void LevelUp()
        {
            _data.Level = ++_level;
            _data.NextLevelExp = _nextLevelExp += EXPERIENCE_STEP;
            _data.StatPoints = _statPoints += STAT_POINTS_PER_LEVEL;
            _data.SkillPoints = _skillPoints += SKILL_POINTS_PER_LEVEL;
        }

        public bool RemoveStatPoint()
        {
            if (_statPoints > 0)
            {
                _data.StatPoints = --_statPoints;
                if (_manager != null)
                {
                    _manager.StatPoints = _statPoints;
                }
                return true;
            }
            return false;
        }

        public void Load(UserData data)
        {
            _data = data;
            if (data.Level > 0)
            {
                _level = data.Level;
            }
            _statPoints = data.StatPoints;
            _skillPoints = data.SkillPoints;
            _expa = data.Expa;
            if (data.NextLevelExp > 0)
            {
                _nextLevelExp = data.NextLevelExp;
            }
        }

        public bool RemoveSkillPoint()
        {
            if (_skillPoints > 0)
            {
                _data.SkillPoints = --_skillPoints;
                if (_manager != null)
                {
                    _manager.SkillPoints = _skillPoints;
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}
