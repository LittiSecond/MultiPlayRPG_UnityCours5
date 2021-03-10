using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class StatsManager : NetworkBehaviour
    {
        #region Fields

        [SyncVar] public int Damage;
        [SyncVar] public int Armor;
        [SyncVar] public int MoveSpeed;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        #endregion
    }
}