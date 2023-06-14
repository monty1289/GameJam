using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.5f; // The speed at which the camera follows the player
    public Vector3 offset; // The offset between the camera and the player

    public GameObject spotlight; // Reference to the spotlight GameObject    

    private Camera mainCamera; // Reference to the main camera

    private void Start()
    {
        mainCamera = Camera.main; // Get the reference to the main camera
    }

    private void LateUpdate()
    {
        if (player == null)
            return; // If the player is null, exit the method

        // Calculate the desired position of the camera based on the player's position and the offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        mainCamera.transform.position = smoothedPosition;

        if (spotlight != null)
        {
            // Set the position of the spotlight to match the player's position
            spotlight.transform.position = player.position;
        } 
    }       
}