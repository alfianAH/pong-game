using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Text loadingText;
    
    /// <summary>
    /// Play game
    /// </summary>
    public void PlayGame(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }
    
    /// <summary>
    /// Load async scene
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while(!asyncLoad.isDone)
        {
            float progress = asyncLoad.progress * 100;
            loadingText.text = $"{progress:00}%";
            yield return null;
        }
    }
    
    /// <summary>
    /// Exit game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Pause or resume game
    /// </summary>
    /// <param name="timeScale"></param>
    public void PauseOrResumeGame(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    /// <summary>
    /// Back to home
    /// </summary>
    public void BackToHome(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }
}
