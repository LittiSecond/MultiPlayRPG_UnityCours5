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

        private UserData _data;

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
                    _data.Equipment.Remove(ItemBase.GetItemID(Items[i]));
                    Items.RemoveAt(i);
                    break;
                }
            }

            Items.Add(item);
            item.Equip(PlayerScriptsConnectorr);
            _data.Equipment.Add(ItemBase.GetItemID(item));

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
                _data.Equipment.Remove(ItemBase.GetItemID(Items[index]));
                Items.RemoveAt(index);
            }
        }

        public void Load(UserData data)
        {
            _data = data;
            for (int i = 0; i < data.Equipment.Count; i++)
            {
                EquipmentItem item = (EquipmentItem)ItemBase.GetItem(data.Equipment[i]);
                Items.Add(item);
                item.Equip(PlayerScriptsConnectorr);
            }
        }

        #endregion

    }
}
