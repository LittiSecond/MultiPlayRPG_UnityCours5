using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class StatsManager : NetworkBehaviour
    {
        #region Fields

        [SyncVar] public float Expa;
        [SyncVar] public float NextLevelExp;
        [SyncVar] public int Damage;
        [SyncVar] public int Armor;
        [SyncVar] public int MoveSpeed;
        [SyncVar] public int Level;
        [SyncVar] public int StatPoints;
 

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