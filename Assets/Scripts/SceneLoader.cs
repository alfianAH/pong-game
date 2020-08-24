using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Play game
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Pong");
    }

    /// <summary>
    /// Exit game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
