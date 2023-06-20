using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite doorOpen;
    public int GemCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenDoor() 
    {
        Debug.Log(GemCount);
        if (GemCount >= 8)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
        }
    }

    public void DoorResponse()
    { 
    
    }
}
