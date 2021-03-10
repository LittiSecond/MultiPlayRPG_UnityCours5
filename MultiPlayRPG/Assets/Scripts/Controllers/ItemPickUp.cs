using UnityEngine;


namespace MultiPlayRPG
{
    public class ItemPickUp : Interactable
    {
        #region Fields

        public Item ItemScriptableObject;
        public float LiveTime = 10.0f;

        private float _liveTimer;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _liveTimer = LiveTime;
        }

        private void Update()
        {
            if (isServer)
            {
                if (LiveTime > 0.0f)
                {
                    _liveTimer -= Time.deltaTime;
                    if (_liveTimer <= 0.0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        #endregion


        #region Methods

        public override bool Interact(GameObject luser)
        {
            return PickUp(luser);
        }

        public bool PickUp(GameObject luser)
        {
            Debug.Log("ItemPickUp::PickUp: pick up " + ItemScriptableObject.Name);
            CharacterOfPlr character = luser.GetComponent<CharacterOfPlr>();
            if (character != null)
            {
                if (character.AddToInventory(ItemScriptableObject))
                {
                    Destroy(gameObject);
                    //return true;
                }
            }

            return false;
        }

        #endregion
    }
}
