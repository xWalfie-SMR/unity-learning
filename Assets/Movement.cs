using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour {
    // Time and intervals
    private float _time;
    private const float Interval = 1f;

    // Rigidbody
    private Rigidbody _rb;

    // Labels
    public TMP_Text speedLabel;
    public TMP_Text fpsLabel;

    // Physics variables
    private Vector3 _velocity;
    private bool _jumpRequested;
    public float jumpSpeed = 8f;
    public float airControlMult = 0.2f;
    
    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, 0.6f);
    }

    // Movement variables
    public float baseSpeed = 7f;
    public float baseAccelSpeed = 7f;
    public float runMultiplier = 2f;

    private float _maxSpeed;
    private float _accelSpeed;

    // Running speed
    private float _runningSpeed;
    private float _runningAccelSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        // Set time to 0 and apply the fps label at start
        _time = 0f;
        fpsLabel.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);

        // Get rigidbody
        _rb = GetComponent<Rigidbody>();

        _runningSpeed = baseSpeed * runMultiplier;
        _runningAccelSpeed = baseAccelSpeed * runMultiplier;
    }

    // FixedUpdate
    private void FixedUpdate() {
        // Jump
        if (IsGrounded() && _jumpRequested) {
            _rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            _jumpRequested = false;
        }
    }

    // Update is called once per frame
    private void Update() {
        // Update time
        _time += Time.deltaTime;

        // Starting direction
        var direction = Vector3.zero;

        // Movement
        if (Keyboard.current.wKey.isPressed) {
            direction += Vector3.forward;
        }
        if (Keyboard.current.aKey.isPressed) {
            direction += Vector3.left;
        }
        if (Keyboard.current.sKey.isPressed) {
            direction += Vector3.back;
        }

        if (Keyboard.current.dKey.isPressed) {
            direction += Vector3.right;
        }
        
        // Check if jump is requested
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

        // Normalize the direction so diagonal movement is the same
        direction.Normalize();

        // Get the target (direction * _maxSpeed) and move towards it
        _velocity = Vector3.MoveTowards(_velocity, direction * _maxSpeed, currentAccel * Time.deltaTime);
        speedLabel.text = "Speed: " + _velocity.magnitude.ToString("F2");

        // Display FPS each interval
        while (_time >= Interval) {
            fpsLabel.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);
            _time -= Interval;
        }

        transform.Translate(_velocity * Time.deltaTime);
    }
}
