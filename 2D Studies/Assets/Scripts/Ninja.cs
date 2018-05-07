using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;
    private bool jump;

    [SerializeField]
    private float jumpForce;
    private bool facingRight;

    // Use this for initialization
    void Start()
    {
        isGrounded = false;
        facingRight = true;
        jump = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = IsGrounded();
        float horizontal = Input.GetAxis("Horizontal");
        HandleInput();
        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
        
        


        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        myAnimator.SetFloat("playerSpeed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    {
        bool flipToRight = horizontal > 0 && !facingRight;
        bool flipToLeft = horizontal < 0 && facingRight;
        bool flip = flipToLeft || flipToRight;
        if (flip)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }

    private bool IsGrounded()
    {
        if (myRigidBody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject) // is colliding with something different from self
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }



}
