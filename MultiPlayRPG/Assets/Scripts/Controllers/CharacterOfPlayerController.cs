using UnityEngine;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public sealed class CharacterOfPlayerController : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private LayerMask _movementMask;

        private CharacterOfPlr _characterOfPlr;
        private Camera _camera;
        private LayerMask _interactMask;
 
        #endregion


        #region UnityMethods

        private void Awake()
        {
            _camera = Camera.main;
            _movementMask = LayerManager.GetLayerMask(Layers.Ground);
            _interactMask = ~LayerManager.GetLayerMask(Layers.PlayersCharacters);
        }

        private void Update()
        {
            if (isLocalPlayer)
            {
                if (_characterOfPlr != null)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 100f, _movementMask))
                        {
                           CmdSetMovePoint(hit.point);
                        }
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 100f, _interactMask))
                        {
                            Interactable interactable = hit.collider.GetComponent<Interactable>();
                            if (interactable != null)
                            {
                                CmdSetFocus(interactable.GetComponent<NetworkIdentity>());
                            }
                        }
                    }
                }
            }

        }

        private void OnDestroy()
        {
            if (_characterOfPlr != null)
            {
                Destroy(_characterOfPlr.gameObject);
            }
        }

        #endregion


        #region Methods

        public void SetCharacter(CharacterOfPlr characterOfPlr, bool isLocalPlayer)
        {
            _characterOfPlr = characterOfPlr;
            if (isLocalPlayer)
            {
                CameraController cameraController = _camera.GetComponent<CameraController>();
                if (cameraController)
                {
                    cameraController.Target = characterOfPlr.transform;
                }
            }
        }

        [Command]
        public void CmdSetMovePoint(Vector3 point)
        {
            _characterOfPlr.SetMovePoint(point);
        }

        [Command]
        public void CmdSetFocus(NetworkIdentity newFocus)
        {
            _characterOfPlr.SetNewFocus(newFocus.GetComponent<Interactable>());
        }

        #endregion


    }
}
