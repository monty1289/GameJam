using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Maze");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
