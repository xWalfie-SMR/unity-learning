using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem {
    public class FpsController : MonoBehaviour {
        // Define the player's transform
        public Transform playerTransform;
        
        // Camera and player position variables
        public float eyeHeight = 1.6f;

        // Mouse variables
        public float mouseSensitivity = 12f;

        public float HorizontalAngle { get; set; }
        public float VerticalAngle { get; set; }

        public Vector2 clamp = new(0f, 85f);

        private void Start() {
            HorizontalAngle = 0f;
            VerticalAngle = 0f;

            var rotation = Quaternion.Euler(VerticalAngle, HorizontalAngle, 0);
            
            Mouse.current.delta.ReadValue();
        }

        private void Update() {
            var mouseDelta = Mouse.current.delta.ReadValue() * (mouseSensitivity * Time.smoothDeltaTime);

            // Store angles from mouse delta
            VerticalAngle += mouseDelta.y;
            HorizontalAngle += mouseDelta.x;

            // Clamp the vertical angle
            VerticalAngle = Mathf.Clamp(VerticalAngle, clamp.x, clamp.y);

            // Get rotation
            var rotation = Quaternion.Euler(-VerticalAngle, HorizontalAngle, 0);
            var playerRotation = Quaternion.Euler(0, HorizontalAngle, 0);
            
            transform.rotation = rotation;
            transform.position = playerTransform.position + Vector3.up * eyeHeight;
            
            playerTransform.rotation = playerRotation;
        }
    }
}
