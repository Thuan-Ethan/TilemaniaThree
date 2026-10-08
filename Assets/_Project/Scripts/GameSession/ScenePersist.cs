using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    
    void Awake()
    {
        // Singleton Pattern
        int numberScenePersists = FindObjectsByType<ScenePersist>(FindObjectsInactive.Exclude).Length;
        if (numberScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersists()
    {
        Destroy(gameObject);
    }
}
