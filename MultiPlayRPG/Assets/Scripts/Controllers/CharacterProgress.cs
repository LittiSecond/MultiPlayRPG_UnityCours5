using System;
using UnityEngine;

namespace MultiPlayRPG
{
    public class CharacterProgress : MonoBehaviour
    {
        #region Fields

        private const float EXPERIENCE_STEP = 100.0f;
        private const int STAT_POINTS_PER_LEVEL = 3;

        private float _expa;
        private float _nextLevelExp;
        private int _level;
        private int _statPoints;

        #endregion


        #region Methods

        public void AddExp(float addExpa)
        {
            _expa += addExpa;
            while (_expa >= _nextLevelExp)
            {
                _expa -= _nextLevelExp;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _level++;
            _nextLevelExp += EXPERIENCE_STEP;
            _statPoints += STAT_POINTS_PER_LEVEL;
        }

        public bool RemoveStatPoint()
        {
            if (_statPoints > 0)
            {
                _statPoints--;
                return true;
            }
            return false;
        }

        #endregion
    }
}
