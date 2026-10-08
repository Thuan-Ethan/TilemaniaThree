using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    void Awake()
    {
        // Singleton Pattern
        int numberGameSessions = FindObjectsByType<GameSessions>(FindObjectsInactive.Exclude).Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        livesText.text = $"Life: {playerLives}";
        scoreText.text = $"Score: {score}";
    }
    
    // When player die -> Reduce number of lives. 
    // If we have no lives -> Restart the game
    public void ProcessPlayerDeath()
    {
        if (playerLives > 0)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = $"Life: {playerLives}";
    }

    void ResetGameSession()
    {
        FindAnyObjectByType<ScenePersist>().ResetScenePersists();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
