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
    }
}
