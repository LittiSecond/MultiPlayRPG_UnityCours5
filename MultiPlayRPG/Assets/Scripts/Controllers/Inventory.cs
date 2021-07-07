using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class Inventory : NetworkBehaviour
    {
        #region Fields

        public event SyncList<Item>.SyncListChanged OnItemChanged;

        private UserData _data;

        //public Transform DropPoint;
        public PlayerScriptsConnector PlayerScriptsConnectorr;
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

        public bool AddItem(Item item)
        {
            if (Items.Count < Space)
            {
                Items.Add(item);
                _data.Inventory.Add(ItemBase.GetItemID(item));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DropItem(Item item)
        {
            //Items.Remove(item);
            CmdDropItem(Items.IndexOf(item));
        }

        [Command]
        private void CmdDropItem(int index)
        {
            if (Items[index] != null)
            {
                Drop(Items[index]);
                RemoveItem(Items[index]);
            }
        }

        private void ItemChanged(SyncList<Item>.Operation op, int itemIndex)
        {
            OnItemChanged(op, itemIndex);
        }

        private void Drop(Item item)
        {
            ItemPickUp pickupitem = Instantiate(item.PickUpPrefab, 
                PlayerScriptsConnectorr.Character.transform.position,
                Quaternion.Euler(0, UnityEngine.Random.Range(0, 360.0f), 0));
            pickupitem.ItemScriptableObject = item;
            NetworkServer.Spawn(pickupitem.gameObject);
        }

        public void UseItem(Item item)
        {
            CmdUseItem(Items.IndexOf(item));
        }


        [Command]
        void CmdUseItem(int index)
        {
            if (Items[index] != null)
            {
                Items[index].Use(PlayerScriptsConnectorr);
            }
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            _data.Inventory.Remove(ItemBase.GetItemID(item) );
        }

        public void Load(UserData data)
        {
            _data = data;
            for (int i = 0; i < data.Inventory.Count; i++)
            {
                Items.Add(ItemBase.GetItem(data.Inventory[i]));
            }
        }

        #endregion
    }
}