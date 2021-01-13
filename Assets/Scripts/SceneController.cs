using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        Scene tempScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(tempScene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
