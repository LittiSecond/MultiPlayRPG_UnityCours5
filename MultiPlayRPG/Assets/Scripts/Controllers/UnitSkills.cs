using System;
using UnityEngine;


namespace MultiPlayRPG
{
    [Serializable]
    public class UnitSkills
    {
        #region Fields

        [SerializeField] private Skill[] _skills;

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

        #endregion
    }
}
