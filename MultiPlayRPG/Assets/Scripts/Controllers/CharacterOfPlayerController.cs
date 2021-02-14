using UnityEngine;


namespace MultiPlayRPG
{
    [RequireComponent(typeof(UnitMotor))]
    public sealed class CharacterOfPlayerController : MonoBehaviour
    {
        #region Fields

        private Camera _camera;
        private UnitMotor _motor;
        private LayerMask _movementMask = 0x0200;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _camera = Camera.main;
            _motor = GetComponent<UnitMotor>();
            CameraController cameraController = _camera.GetComponent<CameraController>();
            if (cameraController)
            {
                cameraController.Target = transform;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, _movementMask))
                {
                    _motor.MoveToPoint(hit.point);
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, _movementMask))
                {
                    Debug.Log("CharacterOfPlayerController::Update: left mouse botton has been clicked.");
                }

            }

        }

        #endregion


        #region Methods

        #endregion


    }
}
