using UnityEngine;
using System;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(Unit))]
    public class UnitDrop : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private DropItem[] _dropItems = new DropItem[0];

        private Unit _unit;

        #endregion


        #region UnityMethods

        public override void OnStartServer()
        {
            _unit = GetComponent<Unit>();
            _unit.EventOnDie += Drop;
        }

        #endregion


        #region Methods

        private void Drop()
        {
            for (int i = 0; i < _dropItems.Length; i++)
            {
                float dropСhance = UnityEngine.Random.Range(0.0f, 100.0f);
                float dropRate = _dropItems[i].Rate;
                //Debug.Log("UnitDrop::Drop: dropСhance = " + dropСhance.ToString() +
                //    " dropRate = " + dropRate.ToString()) ;
                if (dropСhance <= dropRate)
                {
                    ItemPickUp pockupitem = Instantiate(_dropItems[i].Item.PickUpPrefab, transform.position,
                        Quaternion.Euler(0, UnityEngine.Random.Range(0.0f, 360.0f), 0));
                    pockupitem.ItemScriptableObject = _dropItems[i].Item;
                    NetworkServer.Spawn(pockupitem.gameObject);
                }
            }
        }

        #endregion
    }
}