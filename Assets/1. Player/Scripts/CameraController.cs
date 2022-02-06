using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Main.Camera.Enums;
using UnityEngine.InputSystem;


    [RequireComponent(typeof(PlayerController))]
    public class CameraController : MonoBehaviour
    {
        [Header("Camera options")]
        public Transform cameraRotationAxis;
        public Transform cameraTarget;
        public float cameraRotationSmoothTime = 0.15f;

        [Header("Camera aim options")]
        public float sensitivity = 0.15f;
        private Rect _screenRect = Rect.zero;

        [Header("Dependencies")]
        public PlayerController playerController;

        //public CameraDirection CameraDirection { get { return _cameraDirection; } }
        //public CameraDirection _cameraDirection = CameraDirection.Forward;

        //private void Awake()
        //{
        //    //PlayerInputController.input_OnSpinCameraDelegate += OnTurnCamera;
        //}

        private void FixedUpdate()
        {
            OffsetCamera();
            //RotateCamera();
        }

        //private void OnTurnCamera(int direction)
        //{
        //    int newDirectionVal = (int)_cameraDirection + direction;
        //    if (newDirectionVal < 0)
        //        newDirectionVal += 4;
        //    else if (newDirectionVal > 3)
        //        newDirectionVal -= 4;
        //    _cameraDirection = (CameraDirection)newDirectionVal; //change the camera direction
        //}

        private void OffsetCamera()
        {
            _screenRect = new Rect(-Screen.width / 2, -Screen.height / 2, Screen.width, Screen.height);

            if (_screenRect.Contains(playerController.mouseTarget))//if mouse is still in the game window
            {
                Vector3 direction = playerController.mouseTarget - transform.position;
                direction.Normalize();
                cameraTarget.position = direction * playerController.distanceFromCharacterToMouse * sensitivity + transform.position;
            }
        }

        //private void RotateCamera() //rotates the cameraTarget by 'angle' degrees
        //{
        //    cameraRotationAxis.rotation = Quaternion.Lerp(cameraRotationAxis.rotation, Quaternion.Euler(0, (int)_cameraDirection * 90f, 0), cameraRotationSmoothTime);
        //}
    }
