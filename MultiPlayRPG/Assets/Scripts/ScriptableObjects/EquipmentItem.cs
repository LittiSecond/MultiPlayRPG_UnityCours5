using System;
using UnityEngine;


namespace MultiPlayRPG
{
    [CreateAssetMenu(fileName = "New equipment", menuName = "CustomMenu/Inventory/Equipment")]
    public class EquipmentItem : Item
    {
        #region Fields

        public EquipmentSlotType EquipmentSlot;

        public int DamageModifer;
        public int ArmorModifer;
        public int SpeedModifer;

        #endregion


        #region Methods

        public override void Use(PlayerScriptsConnector player)
        {
            player.Inventoryy.RemoveItem(this);
            EquipmentItem oldItem = player.Equipmentt.EquipItem(this);
            if (oldItem != null)
            {
                player.Inventoryy.AddItem(oldItem);
            }
            base.Use(player);
        }

        #endregion
    }
}
