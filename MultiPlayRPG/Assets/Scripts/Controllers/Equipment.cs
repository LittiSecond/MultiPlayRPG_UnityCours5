using UnityEngine.Networking;
using System;

namespace MultiPlayRPG
{
    public class Equipment :NetworkBehaviour
    {
        #region Fields

        public event SyncList<Item>.SyncListChanged OnItemChanged;
        public SyncListItem Items = new SyncListItem();
        public PlayerScriptsConnector PlayerScriptsConnectorr;

        #endregion


        #region UnityMethods

        public override void OnStartLocalPlayer()
        {
            Items.Callback += ItemChanged;
        }

        #endregion


        #region Methods

        private void ItemChanged(SyncList<Item>.Operation op, int itemIndex)
        {
            OnItemChanged(op, itemIndex);
        }

        public EquipmentItem EquipItem(EquipmentItem item)
        {
            EquipmentItem oldItem = null;
            for (int i = 0; i < Items.Count; i++)
            {
                if (((EquipmentItem)Items[i]).EquipmentSlot == item.EquipmentSlot)
                {
                    oldItem = (EquipmentItem)Items[i];
                    oldItem.UnEquip(PlayerScriptsConnectorr);
                    Items.RemoveAt(i);
                    break;
                }
            }

            Items.Add(item);
            item.Equip(PlayerScriptsConnectorr);

            return oldItem;
        }

        public void UnEquipItem(Item item)
        {
            CmdUnequipItem(Items.IndexOf(item));
        }

        [Command]
        private void CmdUnequipItem(int index)
        {
            if (Items[index] != null  && 
                PlayerScriptsConnectorr.Inventoryy.AddItem(Items[index]) )
            {
                ((EquipmentItem)Items[index]).UnEquip(PlayerScriptsConnectorr);
                Items.RemoveAt(index);
            }
        }

        #endregion

    }
}
