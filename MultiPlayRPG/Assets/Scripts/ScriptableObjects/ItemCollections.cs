using UnityEngine;


namespace MultiPlayRPG
{
    [CreateAssetMenu(fileName = "NewItemCollection", menuName = "CustomMenu/Inventory/ItemCollection")]
    public class ItemCollections : ScriptableObject
    {

        public Item[] Items = new Item[0];

    }
}
