using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public float coinsCollected = 0;

    //ground
    public float groundCheckRadius;

    //jump
    public int amountsOfJumps = 1;

    private float movementInputDirection;

    //jump
    private int amountsOfJumpsLeft;

    //ground
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;
    

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;
    private bool canJump;

    //ground
    public Transform groundCheck;
    
    public GameObject coinEffect;


    public PhysicsMaterial2D withFriction;
    public PhysicsMaterial2D noFriction;

    public Text coinText;
    public GameObject gameOver;

    public GameObject projectile;
    public Transform shotPoint;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountsOfJumpsLeft = amountsOfJumps;
    }

    // Update is called once per frame

    void Update()
    {
        CheckInput();
        CheckIfCanJump();
        CheckMovementDirection();
        UpdateAnimations();
        Flip();


        if (movementInputDirection!= 0)
        {
            anim.SetBool("isRunning", true);
            rb.sharedMaterial = noFriction;
        }
        else
        {
            anim.SetBool("isRunning", false);
            rb.sharedMaterial = withFriction;
        }

        if(Input.GetMouseButton(0))
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
        }

    }
    void FixedUpdate()
    {
        
        Applymovement();
        CheckSurroundings();
        
    }


    //checking movement direction and also flipping
    void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            //Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            //Flip();
        }

        /*if (rb.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }*/
    }

    void UpdateAnimations()
    {
        //anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    //checking user Input
    void CheckInput()
    {
         movementInputDirection = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKeyDown("space"))
        {
            Jump();
        }
    } 


    void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            //for double jump
            amountsOfJumpsLeft--;
        }
        
    }


    //applying movement
    void Applymovement()
    {
        rb.velocity = new Vector2(speed * movementInputDirection, rb.velocity.y);
    }

    //flipping the character
    void Flip()
    {
        //isFacingRight = !isFacingRight;
        //transform.Rotate(0.0f, 180.0f, 0.0f);

        Vector3 characterScale = transform.localScale;

        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            characterScale.x = -1;

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            characterScale.x = 1;

        }
        transform.localScale = characterScale;

    }

    // checking surroundings of the player..  I.E. Ground or Ceiling etc
    void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }



    //this code is not required...using it just to see the groundcheck circle
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    //Checking if canJump is true or false

    void CheckIfCanJump()
    {
        /*
         if(isGrounded && rb.velocity.y <= 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        } 
         */
        


        // This code is for double jump
        
        if(isGrounded && rb.velocity.y <= 0)
        {
            amountsOfJumpsLeft = amountsOfJumps;
        }

        if(amountsOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
        

        
        
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            coinsCollected += 1;
            Destroy(other.gameObject);
            Instantiate(coinEffect, transform.position, transform.rotation);
            Destroy(coinEffect);
            print("coins = " + coinsCollected);
            coinText.text = coinsCollected.ToString();


        }

        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            gameOver.SetActive(true);
        }
    }



    
}
