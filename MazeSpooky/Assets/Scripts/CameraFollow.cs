using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;

    public GameObject spotlight;
    public SpriteRenderer darkOverlay;
    public Color overlayColor = new Color(0f, 0f, 0f, 0.5f);

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        mainCamera.transform.position = smoothedPosition;

        if (spotlight != null)
        {
            spotlight.transform.position = player.position;
        }

        if (darkOverlay != null)
        {
            darkOverlay.transform.position = mainCamera.transform.position;
            darkOverlay.color = overlayColor;
        }
    }
}
