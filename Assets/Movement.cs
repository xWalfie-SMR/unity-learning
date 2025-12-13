using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    private float _time;
    private const float Interval = 1f;
    
    public TMP_Text speedLabel;
    public TMP_Text fpsLabel;
    
    private Vector3 _velocity;

    public float maxSpeed = 5f;
    public float accelSpeed = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _time = 0f;
        fpsLabel.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);
    }

    // Update is called once per frame
    private void Update()
    {
        _time += Time.deltaTime;
        
        var direction = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            direction += Vector3.forward;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            direction += Vector3.left;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            direction += Vector3.back;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            direction += Vector3.right;
        }
        
        direction.Normalize();

        _velocity = Vector3.MoveTowards(_velocity, direction * maxSpeed,accelSpeed * Time.deltaTime);
        speedLabel.text = "Speed: " + _velocity.magnitude.ToString("F2");

        while (_time >= Interval)
        {
            fpsLabel.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);
            _time -= Interval;
        }
        
        transform.Translate(_velocity * Time.deltaTime);
    }
}
