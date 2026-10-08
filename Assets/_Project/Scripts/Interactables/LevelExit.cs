using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneLevel + 1);
        }
    }
}
