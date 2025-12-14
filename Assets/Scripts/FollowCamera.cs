using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour {
    // Define the player's transform
    public Transform playerTransform;

    // The speed the camera follows the player at
    public float speed = 90f;
    
    // Camera and player position variables
    private Vector3 _startingPlayerPos;
    private Vector3 _offset;
    public float yOffset = 1f;
    public float zOffset = -7f;
    public Vector2 clamp = new Vector2(-20f, 85f);
    
    // Mouse variables
    public float mouseSensitivity = 12f;
    private float _horizontalAngle;
    private float _verticalAngle;

    private void Start() {
        _offset = new Vector3(0, yOffset, zOffset);
        
        _horizontalAngle = 0f;
        _verticalAngle = 20f;
        
        var rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
        var rotatedOffset = rotation * _offset;
        
        transform.position = playerTransform.position + rotatedOffset;
        transform.LookAt(playerTransform);
        
        Mouse.current.delta.ReadValue();
    }

    private void Update() {
        var mouseDelta = Mouse.current.delta.ReadValue() * (mouseSensitivity * Time.smoothDeltaTime);

        // Store angles from mouse delta
        _verticalAngle += mouseDelta.y;
        _horizontalAngle += mouseDelta.x;
        
        // Clamp the vertical angle
        _verticalAngle = Mathf.Clamp(_verticalAngle, clamp.x, clamp.y);
        
        // Get rotation and apply offset
        var rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
        var rotatedOffset = rotation * _offset;
        
        var targetPos = playerTransform.position + rotatedOffset;
        
        // Move camera towards target position (behind player)
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.LookAt(playerTransform);
    }
}