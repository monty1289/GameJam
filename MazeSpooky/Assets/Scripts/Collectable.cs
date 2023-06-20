using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int lightValue = 1;
    public AudioClip clip;
    public GameObject player;
    public GameObject spotlight;
    private float decreaseRate = 0.02f; // Rate at which the spotlight decreases in size per second

    private UnityEngine.Rendering.Universal.Light2D light2D;
    private float initialOuterRadius;
    private float initialInnerRadius;

    private void Start()
    {
        light2D = spotlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        initialOuterRadius = light2D.pointLightOuterRadius;
        initialInnerRadius = light2D.pointLightInnerRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().lightValue += lightValue;
            AudioManager.Instance.Play(clip, player.transform);

            // Increase the spotlight's outer radius
            if (light2D != null)
            {
                light2D.pointLightOuterRadius += 2f;
                light2D.pointLightInnerRadius += 2f;
            }

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Decrease the spotlight's inner and outer radius over time
        if (light2D != null)
        {
            float newOuterRadius = light2D.pointLightOuterRadius - (decreaseRate * Time.deltaTime);
            float newInnerRadius = light2D.pointLightInnerRadius - (decreaseRate * Time.deltaTime);

            light2D.pointLightOuterRadius = Mathf.Clamp(newOuterRadius, 0f, initialOuterRadius);
            light2D.pointLightInnerRadius = Mathf.Clamp(newInnerRadius, 0f, initialInnerRadius);
        }
    }
}
