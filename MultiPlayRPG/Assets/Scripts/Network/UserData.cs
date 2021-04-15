using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    [Serializable]
    public class UserData
    {

        #region Fields

        public NetworkHash128 CharacterHash = new NetworkHash128();

        public Vector3 CharacterPos;
        public List<int> Inventory = new List<int>();
        public List<int> Equipment = new List<int>();
        public List<int> Skills = new List<int>();

        public float Expa;
        public float NextLevelExp;
        public int Level;
        public int StatPoints;
        public int CurrentHealth;
        public int StatDamage;
        public int StatArmor;
        public int StatMoveSpeed;
        public int SkillPoints;

        #endregion


        #region Methods

        #endregion

    }
}
