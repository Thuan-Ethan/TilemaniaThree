using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    
    
    [SerializeField] Animator myAnimator;
    [SerializeField] Vector2 dieHigh = new Vector2(5f, 10f);
    
    Vector2 moveInput;
    Rigidbody2D rb;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    
    private float gravityScaleCurrent;
    private bool isAlive = true;
    private int  dangerLayer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleCurrent = rb.gravityScale;
        dangerLayer = LayerMask.GetMask("Enemy", "Hazards");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {  return; }
        PlayerRun();
        FlipPlayer();
        ClimbLadder();
        PlayerDie();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) {  return; }
        moveInput = value.Get<Vector2>();
        // print(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {  return; }
        if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.linearVelocity = new Vector2(0f, jumpSpeed);
        }
    }

    void PlayerRun()
    {
        Vector2 playerMove = new Vector2(moveInput.x * playerSpeed, rb.linearVelocity.y); // Player can move right, left but can't up or down
        rb.linearVelocity = playerMove;
        // Set up animation State (Run)
        bool hasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }
    
    void FlipPlayer()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        //transform.localScale = new Vector2(Mathf.Sign(moveInput.x), Mathf.Sign(moveInput.y));
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f); // Y axes not scale
        }
    }

    void ClimbLadder()
    {
        if (myFeetCollider&& myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 climbVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * climbSpeed); // Player move to ladder up and down by Y axes, Can't move on x axes 
            rb.linearVelocity = climbVelocity;
            rb.gravityScale = 0f;
            
            bool hasVerticalSpeed = Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", hasVerticalSpeed);
        }
        else
        {
            rb.gravityScale = gravityScaleCurrent;
            myAnimator.SetBool("isClimbing", false);
        }
    }

    void PlayerDie()
    {
        if (myBodyCollider.IsTouchingLayers(dangerLayer) || myFeetCollider.IsTouchingLayers(dangerLayer))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDying");
            rb.linearVelocity = dieHigh; 
        }
    }
}
