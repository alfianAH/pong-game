using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Text loadingText;
    
        /// <summary>
        /// Play game
        /// </summary>
        public void PlayGame(string sceneName)
        {
            AudioManager.Instance.Play(ListSound.ButtonClick);
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
            AudioManager.Instance.Play(ListSound.ButtonClick);
            Application.Quit();
        }

        /// <summary>
        /// Pause or resume game
        /// </summary>
        /// <param name="timeScale"></param>
        public void PauseOrResumeGame(float timeScale)
        {
            AudioManager.Instance.Play(ListSound.ButtonClick);
            Time.timeScale = timeScale;
        }

        /// <summary>
        /// Back to home
        /// </summary>
        public void BackToHome(string sceneName)
        {
            AudioManager.Instance.Play(ListSound.ButtonClick);
            StartCoroutine(LoadAsyncScene(sceneName));
        }
    }
}
