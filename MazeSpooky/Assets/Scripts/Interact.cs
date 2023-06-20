using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject door;
    public Sprite doorOpen;
    public GameObject interactPopup;
    public AudioClip clip;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int gemcount = door.GetComponent<Door>().GemCount;
        if (inRange && Input.GetKeyDown(interactKey)) {
            if (gemcount >= 8)
            {
                //fire unity event
                door.GetComponent<SpriteRenderer>().sprite = doorOpen;
                
            }
            else {
                AudioManager.Instance.Play(clip, player.transform);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
           inRange = true;
            interactPopup.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            interactPopup.SetActive(false);
        }
    }
}
