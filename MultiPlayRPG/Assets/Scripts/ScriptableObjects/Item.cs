using UnityEngine;


namespace MultiPlayRPG
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "CustomMenu/Inventory/Item")]
    public class Item : ScriptableObject
    {
        #region Fields

        public string Name = "NewItem";
        public Sprite Icon = null;
        public ItemPickUp PickUpPrefab;

        #endregion


        #region Methods

        public virtual void Use()
        {
            Debug.Log("Item::Use: Using " + Name);
        }

        #endregion
    }
}
