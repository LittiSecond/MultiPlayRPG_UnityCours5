using System;
using UnityEngine;


namespace MultiPlayRPG
{
    [CreateAssetMenu(fileName = "New equipment", menuName = "CustomMenu/Inventory/Equipment")]
    public class EquiomentItem : Item
    {
        #region Fields

        public EquipmentSlotType EquipmentSlot;

        public int DamageModifer;
        public int ArmorModifer;
        public int SpeedModifer;

        #endregion


        #region Methods

        public override void Use()
        {


            base.Use();
        }

        #endregion
    }
}
