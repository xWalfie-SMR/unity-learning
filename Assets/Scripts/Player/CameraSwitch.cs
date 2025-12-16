using CameraSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class CameraSwitch : MonoBehaviour {
        public FpsController fpsController;
        public TpsController tpsController;

        private Camera _fpsCamera;
        private Camera _tpsCamera;

        private Movement _movement;
        private int _cameraIndex;

        private void Start() {
            // Get movement component
            _movement = GetComponent<Movement>();
            
            // Cache camera references
            _fpsCamera = fpsController.GetComponent<Camera>();
            _tpsCamera = tpsController.GetComponent<Camera>();
            
            // Set default camera
            _movement.activeCamera = _fpsCamera;
            
            fpsController.enabled = _cameraIndex == 0;
            fpsController.gameObject.SetActive(_cameraIndex == 0);
    
            tpsController.enabled = _cameraIndex == 1;
            tpsController.gameObject.SetActive(_cameraIndex == 1);
        }

        private void Update() {
            if (!Keyboard.current.vKey.wasPressedThisFrame) return;

            switch (_cameraIndex) {
                case 0:
                    // FPS to TPS angle
                    tpsController.HorizontalAngle = fpsController.HorizontalAngle;
                    tpsController.VerticalAngle = fpsController.VerticalAngle;
                    break;
                case 1:
                    // TPS to FPS angle
                    fpsController.HorizontalAngle = tpsController.HorizontalAngle;
                    fpsController.VerticalAngle = tpsController.VerticalAngle;
                    break;
            }

            _cameraIndex = _cameraIndex == 0 ? 1 : 0;
            
            _movement.activeCamera = _cameraIndex == 0
                ? _fpsCamera
                : _tpsCamera;
                
            fpsController.enabled = _cameraIndex == 0;
            fpsController.gameObject.SetActive(_cameraIndex == 0);
                
            tpsController.enabled = _cameraIndex == 1;
            tpsController.gameObject.SetActive(_cameraIndex == 1);
        }
    }
}