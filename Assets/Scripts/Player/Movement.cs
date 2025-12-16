using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Serialization;

namespace Player {
    public class Movement : MonoBehaviour {
        // Rigidbody
        private Rigidbody _rb;

        // Camera
        public Camera activeCamera;

        // Labels
        public TMP_Text speedLabel;
        public TMP_Text fpsLabel;

        // Physics variables
        private Vector3 _velocity;
        private bool _jumpRequested;
        public float jumpHeight = 0.5f;
        public float airControlMult = 0.4f;

        private bool IsGrounded() {
            return Physics.Raycast(transform.position, -Vector3.up, 1.4f);
        }

        // Movement variables
        public float baseSpeed = 7f;
        public float baseAccelSpeed = 7f;
        public float runMultiplier = 2f;
        public float brakeMultiplier = 10f;

        private float _maxSpeed;
        private float _accelSpeed;

        // Running speed
        private float _runningSpeed;
        private float _runningAccelSpeed;

        private void Start() {
            // Get rigidbody
            _rb = GetComponent<Rigidbody>();

            _runningSpeed = baseSpeed * runMultiplier;
            _runningAccelSpeed = baseAccelSpeed * runMultiplier;
        }

        // FixedUpdate
        private void FixedUpdate() {
            // Apply movement
            _rb.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);

            // Jump
            if (!IsGrounded() || !_jumpRequested) return;
            var jumpVelocity = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * jumpHeight);
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpVelocity, _rb.linearVelocity.z);
            _jumpRequested = false;
        }

        private void Update() {
            if (!activeCamera) return;
            
            // Starting direction
            var direction = Vector3.zero;

            // Camera forward without Y
            var cameraForward = activeCamera.transform.forward;
            cameraForward.y = 0;

            // Camera right without Y
            var cameraRight = activeCamera.transform.right;
            cameraRight.y = 0;

            // WASD
            if (Keyboard.current.wKey.isPressed) {
                direction += cameraForward;
            }

            if (Keyboard.current.aKey.isPressed) {
                direction -= cameraRight;
            }

            if (Keyboard.current.sKey.isPressed) {
                direction -= cameraForward;
            }

            if (Keyboard.current.dKey.isPressed) {
                direction += cameraRight;
            }

            // Check if a jump is requested
            if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                _jumpRequested = true;
            }

            // Shift to run
            if (Keyboard.current.shiftKey.isPressed) {
                _maxSpeed = _runningSpeed;
                _accelSpeed = _runningAccelSpeed;
            }
            else {
                _maxSpeed = baseSpeed;
                _accelSpeed = baseAccelSpeed;
            }

            // Check if grounded, then calculate acceleration speed
            var currentAccel = IsGrounded() ? _accelSpeed : _accelSpeed * airControlMult;

            // Normalize the direction so the diagonal movement is the same
            direction.Normalize();

            // Get the target (direction * _maxSpeed) and move towards it
            var targetVelocity = direction * _maxSpeed;
            _velocity = (Vector3.Dot(targetVelocity, _velocity) < 0f) && IsGrounded() ? Vector3.MoveTowards(_velocity, direction * _maxSpeed, (currentAccel * brakeMultiplier) * Time.deltaTime) : Vector3.MoveTowards(_velocity, direction * _maxSpeed, currentAccel * Time.deltaTime);

            speedLabel.text = "Speed: " + _velocity.magnitude.ToString("F2");

            // Display FPS
            fpsLabel.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);

            // Fix player rotation to camera if moving and grounded
            if (_velocity != Vector3.zero && IsGrounded()) {
                transform.rotation = Quaternion.LookRotation(cameraForward);
            }
        }
    }
}