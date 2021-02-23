using UnityEngine;


namespace MultiPlayRPG
{
    public sealed class UnitScriptsConnector : MonoBehaviour
    {
        #region UnityMethods

        private void Start()
        {
            UnitStats unitStats = GetComponent<UnitStats>();
            StatIndicator statIndicator = GetComponentInChildren<StatIndicator>();
            statIndicator.SetHealth(unitStats);
        }

        #endregion
    }
}