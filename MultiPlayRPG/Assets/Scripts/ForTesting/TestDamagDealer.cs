using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public sealed class TestDamagDealer : NetworkBehaviour
    {
        #region Fields

        private Camera _camera;
        private LayerMask _mask;

        private int _damag = 10;
        private int _healing = 10;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _camera = Camera.main;
            _mask = LayerManager.GetLayerMask(Layers.PlayersCharacters, Layers.Enemies);
            if (!isServer)
            {
                this.enabled = false;
            }
        }

        private void Update()
        {
            KeyCode keyCode = 0;

            if (Input.GetKeyDown(KeyCode.M))
            {
                keyCode = KeyCode.M;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                keyCode = KeyCode.H;
            }

            if (keyCode != 0)
            { 
                //Debug.Log("TestDamagDealer->Update:");
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, _mask))
                {
                    ITakerDamag damagReceiver = hit.collider.transform.GetComponent<ITakerDamag>();
                    if (damagReceiver != null)
                    {
                        if (keyCode == KeyCode.M)
                        {
                            damagReceiver.TakeDamag(_damag);
                        }

                        if (keyCode == KeyCode.H)
                        {
                            damagReceiver.TakeHealing(_healing);
                        }

                    }
                }
            }


        }

        #endregion

    }
}