using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelHero : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;


    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void HandleMovement(float horizontal)
    {
        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        myAnimator.SetFloat("playerSpeed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    {
        bool flipToRight = horizontal > 0 && !facingRight;
        bool flipToLeft = horizontal < 0 && facingRight;
        bool flip = flipToLeft || flipToRight;
        if(flip)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }


}
