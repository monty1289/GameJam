using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int lightValue = 1;
    public AudioClip clip;
    public GameObject player;
    public GameObject door;

    public void OnTriggerEnter2D(Collider2D other){

        if(player !=null)
        {
            player.GetComponent<PlayerMovement>().lightValue += lightValue;
            AudioManager.Instance.Play(clip, player.transform);
            door.GetComponent<Door>().GemCount+= 8;
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
