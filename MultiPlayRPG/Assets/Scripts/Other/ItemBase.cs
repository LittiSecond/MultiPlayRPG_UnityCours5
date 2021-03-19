using UnityEngine;
using System.Collections;


namespace MultiPlayRPG
{
    public sealed class ItemBase : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ItemCollections _collectionnLink;
        public static ItemCollections Collections;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Collections != null)
            {
                if (_collectionnLink != Collections)
                {
                    Debug.LogError("ItemBase::Awake: More than one ItemCollection found!");
                }
                return;
            }
            Collections = _collectionnLink;
        }

        #endregion


        #region Methods

        public static int GetItemID(Item item)
        {
            for (int i = 0; i < Collections.Items.Length; i++)
            {
                if (item == Collections.Items[i])
                {
                    return i;
                }
            }

            if (item != null)
            { 
                    Debug.LogError("ItemBase::GetItemID: Item + " + item.Name + " not found.");
            }

            return -1;
        }

        public static Item GetItem(int id)
        {
            Item item = null;

            if (id >= 0 )
            {
                item = Collections.Items[id];
            }

            return item;
        }

        #endregion
    }
}