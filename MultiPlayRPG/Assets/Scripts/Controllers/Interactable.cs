using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class Interactable : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private float _radius = 2.0f;
        public Transform InteractionTransform;


        protected bool _hasInteract = true;

        #endregion


        #region Properties

        public bool HasInteract
        {
            get
            {
                return _hasInteract;
            }
            set
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
            Gizmos.DrawWireSphere(InteractionTransform.position, _radius);
        }

        public virtual float GetInteractDistance(GameObject luser)
        {
            return _radius;
        }

        #endregion


    }
}
