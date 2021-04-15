using System;
using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class UpgradableSkill : Skill
    {
        #region PrivateData

        public delegate void SetLevelDelegate(UpgradableSkill skill, int newLevel);
        public event SetLevelDelegate OnSetLevel;

        #endregion


        #region Fields

        [SyncVar(hook = "LevelHook")] private int _level;

        #endregion


        #region Properties

        public virtual int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                OnSetLevel?.Invoke(this, _level);
            }
        }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        private void LevelHook(int newLevel)
        {
            _level = newLevel;
        }

        #endregion
    }
}
