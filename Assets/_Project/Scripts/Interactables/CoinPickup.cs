using System;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    
    [SerializeField] int pointsForCoinPickup = 100;
    
    bool hasCollided = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&  !hasCollided)
        {
            hasCollided = true;
            FindAnyObjectByType<GameSessions>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position); // Play the Audio at point where player pickup Coins
            gameObject.SetActive(false); // If u pick up coin then u can't pickup it agains
            Destroy(gameObject);
        }   
    }
}
