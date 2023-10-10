using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void LoadScene(string sceneToLaod)
    {
        SceneManager.LoadScene(sceneToLaod);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
