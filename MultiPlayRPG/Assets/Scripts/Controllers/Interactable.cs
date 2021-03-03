using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class Interactable : NetworkBehaviour
    {
        #region Fields

        public Transform _interactionTransform;
        public float _radius = 2.0f;

        private bool _hasInteract = true;

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


        #region Methods

        public virtual bool Interact(GameObject luser)
        {
            return false;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_interactionTransform.position, _radius);
        }


        #endregion


    }
}
