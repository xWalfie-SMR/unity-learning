using CameraSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class CameraSwitch : MonoBehaviour {
        public FPSController fpsController;
        public TPSController tpsController;

        private int _cameraIndex = 0;

        private void Start() {
            fpsController.enabled = true;
            fpsController.gameObject.SetActive(true);
            
            tpsController.enabled = false;
            tpsController.gameObject.SetActive(false);
        }

        private void Update() {
            if (!Keyboard.current.vKey.wasPressedThisFrame) return;
            
            _cameraIndex = _cameraIndex == 0 ? 1 : 0;
                
            fpsController.enabled = _cameraIndex == 0;
            fpsController.gameObject.SetActive(_cameraIndex == 0);
                
            tpsController.enabled = _cameraIndex == 1;
            tpsController.gameObject.SetActive(_cameraIndex == 1);
        }
    }
}