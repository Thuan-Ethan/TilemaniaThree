using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelDelayExit = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }
    
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(levelDelayExit);
        int currentSceneLevel = SceneManager.GetActiveScene().buildIndex;
        int nextSceneLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneLevel = 0;
        }
        SceneManager.LoadScene(nextSceneLevel);
    }
}
