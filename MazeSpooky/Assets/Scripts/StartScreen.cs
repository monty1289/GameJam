using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
     public void ChangeScene()
    {
        SceneManager.LoadScene("Maze");
    }

    public void ExitGame()
    {
        
        // else
        Application.Quit();    }


}
