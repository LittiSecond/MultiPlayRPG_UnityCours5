using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public sealed class SyncListItem : SyncList<Item>
    {

        protected override Item DeserializeItem(NetworkReader reader)
        {
            return ItemBase.GetItem(reader.ReadInt32());
        }

        protected override void SerializeItem(NetworkWriter writer, Item item)
        {
            writer.Write(ItemBase.GetItemID(item));
        }
    }
}
