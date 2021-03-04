using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace MultiPlayRPG
{
    public class Inventory : NetworkBehaviour
    {
        #region Fields

        public event SyncList<Item>.SyncListChanged OnItemChanged;

        public Transform DropPoint;
        public SyncListItem Items = new SyncListItem();
        public int Space = 17;


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
        private void CmdRemoveItem(int index)
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

        private void Drop(Item item)
        {
            ItemPickUp pickupitem = Instantiate(item.PickUpPrefab, DropPoint.position,
                Quaternion.Euler(0, UnityEngine.Random.Range(0, 360.0f), 0));
            pickupitem.ItemScriptableObject = item;
            NetworkServer.Spawn(pickupitem.gameObject);
        }

        #endregion
    }
}