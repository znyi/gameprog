using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    private bool isGrounded;
    public float checkRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        processInputs();
        //animate
        animate();
        // //move
        // move();
    }

    private void FixedUpdate() { //better for handling physics, can be called multiple times per update frame, 
        //check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        //move
        move();
    }

    private void Awake(){ //called after all objects are initialized, called in a random order.
        rb = GetComponent<Rigidbody2D>(); //get the attached game object
    }

    private void processInputs(){
    moveDirection = Input.GetAxis("Horizontal"); //-1~1
    if(Input.GetButtonDown("Jump") && isGrounded){
        isJumping = true;
        }
    }

    private void animate(){
        if(moveDirection> 0 && !facingRight){
            flipCharacter();
        }
        else if (moveDirection < 0 && facingRight){
            flipCharacter();
        }
    }

    private void move(){
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping){
            rb.AddForce(new Vector2(0f, jumpForce));
        Debug.Log("button jump force: "+ jumpForce);
        }
        isJumping = false;
    }

    private void flipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

