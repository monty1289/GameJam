using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable : MonoBehaviour
{
    public int lightValue = 1;
    public AudioClip clip;
    public GameObject player;
    public GameObject door;    
    public GameObject spotlight;
    public Transform MaxScore;
    public int score = 0;
    public TMP_Text scoreText;
    private float decreaseRate = 0.02f; // Rate at which the spotlight decreases in size per second
    public Image image;
    private float activationRadius = 0.8f;

    private UnityEngine.Rendering.Universal.Light2D light2D;
    private float initialOuterRadius;
    private float initialInnerRadius;

    private CanvasGroup canvasGroup;
    private float alphaIncrement = 0.2f; // Increment value for alpha increase

    private void Start()
    {
        light2D = spotlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        initialOuterRadius = light2D.pointLightOuterRadius;
        initialInnerRadius = light2D.pointLightInnerRadius;

        // Get the CanvasGroup component or add it if it doesn't exist
        canvasGroup = image.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = image.gameObject.AddComponent<CanvasGroup>();
        }

        // Set initial alpha to 0
        canvasGroup.alpha = 0f;
        UpdateScoreText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().lightValue += lightValue;
            AudioManager.Instance.Play(clip, player.transform);
            door.GetComponent<Door>().GemCount+= 8;
            UnityEngine.Rendering.Universal.Light2D light2D = spotlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

            if (light2D != null)
            {
                light2D.pointLightOuterRadius += 2f;
                light2D.pointLightInnerRadius += 2f;             
            }

            Destroy(gameObject);
            score++;
            UpdateScoreText();
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Outer Radius: " + light2D.pointLightOuterRadius);
        // Decrease the spotlight's inner and outer radius over time
        if (light2D != null)
        {
            float newOuterRadius = light2D.pointLightOuterRadius - (decreaseRate * Time.deltaTime);
            float newInnerRadius = light2D.pointLightInnerRadius - (decreaseRate * Time.deltaTime);

            light2D.pointLightOuterRadius = Mathf.Clamp(newOuterRadius, 0f, initialOuterRadius);
            light2D.pointLightInnerRadius = Mathf.Clamp(newInnerRadius, 0f, initialInnerRadius);
            
            // Activate the image if it's not already active and the condition is met
            if (light2D.pointLightOuterRadius <= activationRadius && canvasGroup.alpha < 1f)
            {
                // Gradually increase the alpha of the image over time with a slower increment
                canvasGroup.alpha += alphaIncrement * Time.deltaTime;

                //Debug.Log("Image Alpha: " + canvasGroup.alpha);
            }
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Gems: " + score.ToString() + "/" + MaxScore.childCount.ToString();
    }
}
