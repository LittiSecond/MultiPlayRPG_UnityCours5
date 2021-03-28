﻿using System;
using System.Collections.Generic;

using UnityEngine;

namespace MultiPlayRPG
{
    [Serializable]
    public class UserData
    {

        #region Fields

        public Vector3 CharacterPos;
        public List<int> Inventory = new List<int>();
        public List<int> Equipment = new List<int>();

        public float Expa;
        public float NextLevelExp;
        public int Level;
        public int StatPoints;
        public int CurrentHealth;
        public int StatDamage;
        public int StatArmor;
        public int StatMoveSpeed;

        #endregion


        #region Methods

        #endregion

    }
}