using System;
using UnityEngine;

namespace MultiPlayRPG
{
    [Serializable]
    public struct DropItem
    {
        public Item Item;
        [Range(0, 100)]
        public float Rate;
    }
}
