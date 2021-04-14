using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


namespace MultiPlayRPG
{
    public sealed class CharacterOfPlayerController : NetworkBehaviour
    {
        #region Fields

        private const float RAY_CAST_DISTANCE = 98.037f;

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
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, RAY_CAST_DISTANCE, _movementMask))
                            {
                                CmdSetMovePoint(hit.point);
                            }
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, RAY_CAST_DISTANCE, _interactMask))
                            {
                                Interactable interactable = hit.collider.GetComponent<Interactable>();
                                if (interactable != null)
                                {
                                    CmdSetFocus(interactable.GetComponent<NetworkIdentity>());
                                }
                            }
                        }
                    }

                    if (EventSystem.current.currentSelectedGameObject == null)
                    {
                        if (Input.GetButtonDown("Skill1")) CmdUseSkill(0);
                        if (Input.GetButtonDown("Skill2")) CmdUseSkill(1);
                        if (Input.GetButtonDown("Skill3")) CmdUseSkill(2);
                    }
                }
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
                SkillsPanelUI.Instance.SetSkills(_characterOfPlr.UnitSkills);
            }
        }

        [Command]
        public void CmdSetMovePoint(Vector3 point)
        {
            if (!_characterOfPlr.UnitSkills.InCast)
            {
                _characterOfPlr.SetMovePoint(point);
            }
        }

        [Command]
        public void CmdSetFocus(NetworkIdentity newFocus)
        {
            if (!_characterOfPlr.UnitSkills.InCast)
            {
                _characterOfPlr.SetNewFocus(newFocus.GetComponent<Interactable>());
            }
        }

        [Command]
        void CmdUseSkill(int skillIndex)
        {
            if (!_characterOfPlr.UnitSkills.InCast) _characterOfPlr.UseSkill(skillIndex);
        }

        #endregion


    }
}
