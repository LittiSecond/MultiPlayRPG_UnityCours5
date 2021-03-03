using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class TestDamagDealer : NetworkBehaviour
    {
        #region Fields

        private Camera _camera;
        private LayerMask _mask = 0x0400;

        private int _damag = 10;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _camera = Camera.main;
            if (!isServer)
            {
                this.enabled = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M) )
            {
                Debug.Log("TestDamagDealer->Update:");
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, _mask))
                {
                    ITakerDamag damagReceiver = hit.collider.transform.GetComponent<ITakerDamag>();
                    if (damagReceiver != null)
                    {
                        damagReceiver.TakeDamag(_damag);
                    }
                }
            }
        }

        #endregion

    }
}