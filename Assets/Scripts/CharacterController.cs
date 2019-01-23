using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    private Rigidbody2D theRigidBody;
    //sets a number for the horizontal speed of the player
    public float horizontalSpeed;
    //is a bool for if jumping is through or false
    public bool doJump;
    //sets a number so we can see how many times we have jumped
    public int inJump;
    //sets a true or false for it we are on the ground
    public bool grounded;
    //sets a number for the radius to detect the ground under the player
    public float groundRadius;
    //sets a masking layer so we know what is the ground and what is not
    public LayerMask whatIsGround;
    //sets a number for hspeed
    public float hSpeed;
    //sets a prefab in the script of the bullet so we can use it in the player code
    public GameObject bulletPrefab;
    //sets a gameobject of the transform of the gun that is parented to the player
    public Transform gun;
    //checks if we are on the ground
    [Tooltip("the point around which we will check are we on the ground")]
    public Transform groundCheck;
    //sets the y force of the jump
    [Tooltip("The y force of the jump")]
    public float jumpForce;
    //true or false for if we are facing right or not
    private bool facingRight;


    // Use this for initialization
    void Start ()
    {
        theRigidBody = GetComponent<Rigidbody2D>();
        facingRight = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        doJump = Input.GetKeyDown(KeyCode.UpArrow);


        // Get the value of the "Horizontal! axis, this will be a value between 
        // 1 and -1
        //this allows us to make a game object on the ground check game object parented to the player so that this lets us know we are grounded or not
        float hAxis = Input.GetAxisRaw("Horizontal");

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;


        // If the value of the horizontal axis in not 0 then the user/player is
        // moving the joystick or pressing the arrow keys either left or right
        //this allows us to make sure that if we are not on the h axis and that we are ground that we can set the flips for our character
        //if the h axis is greater than 0 and not facing right we flip and vice verse
        if (hAxis != 0 && grounded)
        {

            if ((hAxis > 0) && (!facingRight))
            {
                flip();
            }
            else if ((hAxis < 0) && (facingRight))
            {
                flip();
            }

            theRigidBody.velocity = new Vector2(horizontalSpeed * hAxis, theRigidBody.velocity.y);

        }
        else if (!grounded)
        {
            theRigidBody.velocity = new Vector2(theRigidBody.velocity.x, theRigidBody.velocity.y);
        }
        else
        {
            theRigidBody.velocity = new Vector2(0, theRigidBody.velocity.y);

        }
    }

    private void FixedUpdate()
    //in this fixed update funtion we are setting parentheses for if statements
    //in this if we are do a jump and we are grounded and our injump is 0 and or if we do jump and or we have jump less than 2 times
    //it will allow us to jump
    //this enables a double jump feature
    //if our rigidbody.y is set to 0 and we are grounded then it will reset our injump to 0
    {
        if ((doJump && grounded && (inJump == 0)) || (doJump && (inJump < 2)))
        {
            Vector2 jf = new Vector2(0, jumpForce);
            theRigidBody.AddForce(jf, ForceMode2D.Impulse);
            inJump++;
        }

        if (theRigidBody.velocity.y == 0 && grounded)
        {
            inJump = 0;
        }
    }

    void flip()
    {
        // Toggle the value of facingRight i.e. put it equal to what
        // it is not currently
        facingRight = !facingRight;

        // Get a compy of the scale property from the Transporm compnent
        Vector3 theScale = gameObject.transform.localScale;

        // Multiply the x component of scale by -1 to 'flip' it.
        theScale.x *= -1;

        // Store the modified value back
        transform.localScale = theScale;
    }
}
