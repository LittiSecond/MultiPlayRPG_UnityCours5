
using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    [System.Serializable]
    public sealed class Stat
    {
        #region Fields

        public delegate void StatChanged(int value);
        public event StatChanged OnStatChanged;

        [SerializeField] private int _baseValue;

        #endregion


        #region Methods

        public int GetValue()
        {
            return _baseValue;
        }

        #endregion

    }
}
