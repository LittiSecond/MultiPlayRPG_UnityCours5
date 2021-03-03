using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class Interactable : NetworkBehaviour
    {
        #region Fields

        public Transform InteractionTransform;
        public float Radius = 2.0f;

        protected bool _hasInteract = true;

        #endregion


        #region Properties

        public bool HasInteract
        {
            get
            {
                return _hasInteract;
            }
            protected set
            {
                _hasInteract = value;
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            if (InteractionTransform == null)
            {
                InteractionTransform = transform;
            }
        }

        #endregion


        #region Methods

        public virtual bool Interact(GameObject luser)
        {
            return false;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(InteractionTransform.position, Radius);
        }


        #endregion


    }
}
