using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem {
    public class TpsController : MonoBehaviour {
        // Define the player's transform
        public Transform playerTransform;

        // The speed the camera follows the player at
        public float speed = 90f;

        // Camera and player position variables
        private Vector3 _startingPlayerPos;
        private Vector3 _offset;
        public float yOffset = 1;
        public float zOffset = -7;
        public Vector2 clamp = new(0f, 85f);

        // Mouse variables
        public float mouseSensitivity = 12f;

        public float HorizontalAngle { get; set; }
        public float VerticalAngle { get; set; }

        private void Start() {
            _offset = new Vector3(0, yOffset, zOffset);

            HorizontalAngle = 0f;
            VerticalAngle = 20f;

            var rotation = Quaternion.Euler(VerticalAngle, HorizontalAngle, 0);
            var rotatedOffset = rotation * _offset;

            transform.position = playerTransform.position + rotatedOffset;
            transform.LookAt(playerTransform);

            Mouse.current.delta.ReadValue();
        }

        private void Update() {
            var mouseDelta = Mouse.current.delta.ReadValue() * (mouseSensitivity * Time.smoothDeltaTime);

            // Store angles from mouse delta
            VerticalAngle += mouseDelta.y;
            HorizontalAngle += mouseDelta.x;

            // Clamp the vertical angle
            VerticalAngle = Mathf.Clamp(VerticalAngle, clamp.x, clamp.y);

            // Get rotation and apply offset
            var rotation = Quaternion.Euler(-VerticalAngle, HorizontalAngle, 0);
            var rotatedOffset = rotation * _offset;

            var targetPos = playerTransform.position + rotatedOffset;

            // Move camera towards target position (behind player)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.LookAt(playerTransform);
        }
    }
}