using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //Temporary as the mech/pilot selection screen is not done yet
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "MainScene");
    }
}
