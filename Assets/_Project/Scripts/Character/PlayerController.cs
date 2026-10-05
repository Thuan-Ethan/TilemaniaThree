using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 15f;
    
    Vector2 moveInput;
    Rigidbody2D rb; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRun();
        flipPlayer();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // print(moveInput);
    }

    void playerRun()
    {
        Vector2 playerMove = new Vector2(moveInput.x * playerSpeed, rb.linearVelocity.y); // Player can move right, left but can't up or down
        rb.linearVelocity = playerMove;
    }
    
    void flipPlayer()
    {
        bool hasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        //transform.localScale = new Vector2(Mathf.Sign(moveInput.x), Mathf.Sign(moveInput.y));
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f); // Y axes not scale
        }
    }
    
}
