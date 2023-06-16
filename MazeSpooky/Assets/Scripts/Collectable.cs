using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Collectable : MonoBehaviour
{
    public int lightValue = 1;
    public AudioClip clip;
    public GameObject player;
    public GameObject spotlight;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().lightValue += lightValue;
            AudioManager.Instance.Play(clip, player.transform);
            UnityEngine.Rendering.Universal.Light2D light2D = spotlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

            if (light2D != null)
            {
                light2D.pointLightOuterRadius += 0.5f;
            }

            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}

