using UnityEngine;


namespace MultiPlayRPG
{
    public class ItemPickUp : Interactable
    {
        #region Fields

        public Item ItemScriptableObject;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override bool Interact(GameObject luser)
        {
            return PickUp(luser);
        }

        public bool PickUp(GameObject luser)
        {
            Debug.Log("ItemPickUp::PickUp: pick up " + ItemScriptableObject.Name);
            Destroy(gameObject);
            return false;
        }

        #endregion
    }
}
