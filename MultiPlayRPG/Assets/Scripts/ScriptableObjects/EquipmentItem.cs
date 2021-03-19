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

        public virtual void Equip(PlayerScriptsConnector connector)
        {
            if (connector != null)
            {
                UnitStats stats = connector.Character.Stats;
                if (DamageModifer != 0)
                {
                    stats.Damag.AddModifer(DamageModifer);
                }
                if (ArmorModifer != 0)
                {
                    stats.Armor.AddModifer(ArmorModifer);
                }
                if (SpeedModifer != 0)
                {
                    stats.MoveSpeed.AddModifer(SpeedModifer);
                }

            }
        }

        public virtual void UnEquip(PlayerScriptsConnector connector)
        {
            if (connector != null)
            {
                UnitStats stats = connector.Character.Stats;
                if (DamageModifer != 0)
                {
                    stats.Damag.RemoveModifer(DamageModifer);
                }
                if (ArmorModifer != 0)
                {
                    stats.Armor.RemoveModifer(ArmorModifer);
                }
                if (SpeedModifer != 0)
                {
                    stats.MoveSpeed.RemoveModifer(SpeedModifer);
                }
            }
        }


        #endregion
    }
}
