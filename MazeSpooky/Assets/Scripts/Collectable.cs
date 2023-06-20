using UnityEngine;
using TMPro;

public class Collectable : MonoBehaviour
{
    public int lightValue = 1;
    public AudioClip clip;
    public GameObject player;
    public GameObject spotlight;
    public Transform MaxScore;
    public int score = 0;
    public TMP_Text scoreText;
    private float decreaseRate = 0.02f; // Rate at which the spotlight decreases in size per second

    private UnityEngine.Rendering.Universal.Light2D light2D;
    private float initialOuterRadius;
    private float initialInnerRadius;

    private void Start()
    {
        light2D = spotlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        initialOuterRadius = light2D.pointLightOuterRadius;
        initialInnerRadius = light2D.pointLightInnerRadius;
        UpdateScoreText();
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
                light2D.pointLightOuterRadius += 1.5f;
                light2D.pointLightInnerRadius += 1.5f;
            }

            Destroy(gameObject);
            score++;
            UpdateScoreText();
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

      private void UpdateScoreText()
    {
        scoreText.text = "Gems: " + score.ToString() + "/" + MaxScore.childCount.ToString();
    }
}
