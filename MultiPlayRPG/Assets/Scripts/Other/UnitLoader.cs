using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class UnitLoader : NetworkBehaviour
    {
        #region Fields

        [SerializeField] GameObject _unitPrefab;

        #endregion


        #region UnityMethods

        public override void OnStartServer()
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            NetworkServer.SpawnWithClientAuthority(unit, gameObject);
        }

        #endregion
    }
}