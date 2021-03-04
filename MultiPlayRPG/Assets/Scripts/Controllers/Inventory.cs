using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace MultiPlayRPG
{
    public class Inventory : NetworkBehaviour
    {
        #region Fields

        public event SyncList<Item>.SyncListChanged OnItemChanged;

        public int Space = 17;
        public SyncListItem Items = new SyncListItem();

        #endregion


        #region UnityMethods

        public override void OnStartLocalPlayer()
        {
            //base.OnStartLocalPlayer();
            Items.Callback += ItemChanged;
        }

        #endregion


        #region Methods

        public bool Add(Item item)
        {
            if (Items.Count < Space)
            {
                Items.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove(Item item)
        {
            Items.Remove(item);
        }

        [Command]
        void CmdRemoveItem(int index)
        {
            if (Items[index] != null)
            {
                Items.RemoveAt(index);
            }
        }

        private void ItemChanged(SyncList<Item>.Operation op, int itemIndex)
        {
            OnItemChanged(op, itemIndex);
        }

        #endregion
    }
}