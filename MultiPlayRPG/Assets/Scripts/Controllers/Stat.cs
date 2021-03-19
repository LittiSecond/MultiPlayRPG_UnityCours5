using UnityEngine;
using System.Collections.Generic;


namespace MultiPlayRPG
{
    [System.Serializable]
    public sealed class Stat
    {
        #region Fields

        public delegate void StatChanged(int value);
        public event StatChanged OnStatChanged;

        [SerializeField] private int _baseValue;

        private List<int> _modifers = new List<int>();

        #endregion


        #region Methods

        public int GetValue()
        {
            int finalValue = _baseValue;
            _modifers.ForEach(x => finalValue += x);
            return finalValue;
        }

        public void AddModifer(int modifer)
        {
            if (modifer != 0)
            {
                _modifers.Add(modifer);
                OnStatChanged?.Invoke(GetValue());
            }
        }

        public void RemoveModifer(int modifer)
        {
            if (modifer != 0)
            {
                _modifers.Remove(modifer);
                OnStatChanged?.Invoke(GetValue());
            }
        }

        #endregion

    }
}
