using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 1f;
    
    Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemySpeed = -enemySpeed;
        FlipEnemy();
    }

    void EnemyMove()
    {
        rb.linearVelocity = new Vector2(enemySpeed, 0f);
    }

    void FlipEnemy()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.linearVelocity.x)), 1f); // Y axes not scale
        }
    }
}
