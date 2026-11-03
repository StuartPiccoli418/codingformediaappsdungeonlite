using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Drag your Player object here in the Inspector.")]
    public Transform target;

    [Tooltip("The offset distance of the camera relative to the player.")]
    public Vector3 offset = new Vector3(0, 12, -8);

    [Tooltip("The fixed rotation of the camera.")]
    public Vector3 cameraRotation = new Vector3(55, 0, 0);
    
    void Start()
    {
        // Apply the static rotation to the camera at the start
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
    void LateUpdate()
    {
        if (target != null)
        {
            // Keep the camera locked to the player's position plus the offset
            transform.position = target.position + offset;

            // Optional: Make the camera look down at the player
            transform.LookAt(target);

            // Apply the fixed rotation
            transform.rotation = Quaternion.Euler(cameraRotation);
        }
    }
}