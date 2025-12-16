using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem {
    public class FPSController : MonoBehaviour {
        // Define the player's transform
        public Transform playerTransform;
        
        // Camera and player position variables
        public float eyeHeight = 1.6f;

        // Mouse variables
        public float mouseSensitivity = 12f;
        private float _horizontalAngle;
        private float _verticalAngle;
        
        public Vector2 clamp = new(0f, 85f);

        private void Start() {
            _horizontalAngle = 0f;
            _verticalAngle = 0f;

            var rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
            
            Mouse.current.delta.ReadValue();
        }

        private void Update() {
            var mouseDelta = Mouse.current.delta.ReadValue() * (mouseSensitivity * Time.smoothDeltaTime);

            // Store angles from mouse delta
            _verticalAngle += mouseDelta.y;
            _horizontalAngle += mouseDelta.x;

            // Clamp the vertical angle
            _verticalAngle = Mathf.Clamp(_verticalAngle, clamp.x, clamp.y);

            // Get rotation
            var rotation = Quaternion.Euler(-_verticalAngle, _horizontalAngle, 0);
            var playerRotation = Quaternion.Euler(0, _horizontalAngle, 0);
            
            transform.rotation = rotation;
            transform.position = playerTransform.position + Vector3.up * eyeHeight;
            
            playerTransform.rotation = playerRotation;
        }
    }
}
