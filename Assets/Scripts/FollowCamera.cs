using UnityEngine;
using UnityEngine.Serialization;

public class FollowCamera : MonoBehaviour {
    // Define the player's transform and rigidbody, and define the camera
    public Transform playerTransform;
    public Rigidbody playerRigidBody;

    private Camera _camera;

    // The speed the camera follows the player at
    [FormerlySerializedAs("Speed")] public float speed = 90f;
    
    // Camera and player position variables
    private Vector3 _startingPlayerPos;
    public float yOffset = 1f;
    public float zOffset = -7f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Get the camera
        _camera = GetComponent<Camera>();
        
        // Get player starting position
        _startingPlayerPos = playerTransform.position;
        
        // Move camera behind player
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
    }

    // Update is called once per frame
    private void Update() {
        var targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.LookAt(playerTransform);
    }
}
