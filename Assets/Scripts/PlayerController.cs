using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float moveVelocity;
    public float jumpHeight;
    float currentPlayerSpeed;
    private Rigidbody2D thisRigidbody;

    public Transform firingPoint;               // to get the position to fire from. holds the location of an object
    public GameObject laserBeamObj;         // for holding a prefab of laser

    Animator anim;                      // for animation

    // for checking if the player is grounded
    private bool grounded;
    public Transform groundCheckObj;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    // for knocking the player back on hitting enemies or contaptions
    public bool rightKnock;
    public float knockbackLength;
    public float knockbackDelay;
    public float knockbackCounter;

    // delaying fire rate
    public float fireDelay;
    public float fireCounter;

    // for checking if an item is near the player to push or pull around
    public Transform itemNearCheck;
    public float itemNearCheckRadius;
    public LayerMask whatIsItem;
    public  bool nearItem;
    public bool noActionOnLeft;
    public bool noActionOnRight;
    Box box;                        // for calling a method in box inorder to move the box relative to the player
    public GameObject moveableBox;
    public bool onLeftOfItem;
    //public bool onBox;
	// Use this for initialization
	void Start () {

        thisRigidbody = GetComponent<Rigidbody2D>();
        currentPlayerSpeed = moveSpeed;
        anim = GetComponent<Animator>();
        knockbackCounter = 0;
        fireCounter = 0;
    
        anim.SetBool("Dead", false);

        box = FindObjectOfType<Box>();

        noActionOnLeft = true;
        noActionOnRight = true;
	}



    void FixedUpdate() {

      // flipping the player on going left and right
        // if player moving left
        // noActionOnLeft and noActionOnRight are used so that player dosent flip when pulling from either sides of a box see below code for moving box
        if (thisRigidbody.velocity.x < 0 && noActionOnLeft ) {
            // flip player
            thisRigidbody.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // if player moving right
        if (thisRigidbody.velocity.x > 0 && noActionOnRight ) {
            // donot flip player
            thisRigidbody.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        Move( Input.GetAxis( "Horizontal" ) );

       

     
    }   // END OF FIXEDUPDATE();



	// Update is called once per frame
	void Update () {

        grounded = Physics2D.OverlapCircle( groundCheckObj.position, groundCheckRadius, whatIsGround );

        // for checking if the player is near an item or not
        nearItem = Physics2D.OverlapCircle(itemNearCheck.position, itemNearCheckRadius, whatIsItem);

       // onBox = Physics2D.OverlapCircle(groundCheckObj.position, groundCheckRadius, whatIsItem);

        // when player collides with enemy or other knockbackCounter = knockbackLength thus going to the else part
        // until no collision player can move freely.
        if (knockbackCounter <= 0) {
            // moving the player
            thisRigidbody.velocity = new Vector2(moveVelocity * Time.deltaTime, thisRigidbody.velocity.y);
        }
        else {
            // if player knocked from right then player is pushed back to the left
            if (rightKnock) {
                thisRigidbody.velocity = new Vector2(-knockbackLength * Time.deltaTime, knockbackLength * Time.deltaTime );
            }
                // if player knocked from the left the the player is puched back to the right.
            else if (!rightKnock) {
                thisRigidbody.velocity = new Vector2(knockbackLength * Time.deltaTime, knockbackLength * Time.deltaTime);
            }
            // small amt of time is set to knockbackCounter for the pushing back action on collision.
            knockbackCounter -= Time.deltaTime;
        }

        // Walking animation
        anim.SetFloat("Speed", Mathf.Abs( thisRigidbody.velocity.x) );

        // On hitting the key 
        if (Input.GetButtonDown( "Fire1" ) ) {
            Fire();
        }

        // JUMPING.
        // if the player is grounded = true then we need idle animation thus the below code.
        anim.SetBool("Grounded", grounded);

        // so that to work the blend tree for the jump.
        anim.SetFloat("VerticalSpeed", thisRigidbody.velocity.y);

        // if user clicks on space( jump) and if the player is grounded or if player clicks jump and is on the box then jump
        if ( (Input.GetButtonDown( "Jump")  && grounded ) ) {//|| ( Input.GetButtonDown( "Jump") && onBox) ) {

            // grounded = flase thus show the jumping animation
            anim.SetBool("Grounded", grounded);
//            anim.SetBool("onBox", onBox);
            Jump();
        }

        fireCounter -= Time.deltaTime;

      
        // testing
        // moving objects around if near the item and if player is grounded
        if (nearItem && grounded) {

            box.PlayerAction();

            // To knwo if the player is on right or left side of a box 
            if (transform.position.x < moveableBox.transform.position.x) {
                onLeftOfItem = true;
            }
            else {
                onLeftOfItem = false;
            }


            // action key is right control
            if (Input.GetKey(KeyCode.RightControl))  {

                // Push if on left & moving & velocity > 0
                if (onLeftOfItem && anim.GetFloat("Speed") > .01 && thisRigidbody.velocity.x > 0) {
                    anim.SetBool("Pushing", true);
                    anim.SetBool("Pulling", false);

                }
                // Pull if on left & moving & velocity < 0
                else if (onLeftOfItem && anim.GetFloat("Speed") > .01 && thisRigidbody.velocity.x < 0) {
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Pushing", false);
                    noActionOnLeft = false;              // dont flip to left side so as to show pulling in left direction
                    // so that player pulls the box to  left if  ctrl key is pressed with left arrow
                }

                // Pull if on right & moving & velocity > 0
                if (!onLeftOfItem && anim.GetFloat("Speed") > .01 && thisRigidbody.velocity.x > 0)
                {
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Pushing", false);
                    noActionOnRight = false;        // dont flip to riht side so as to show pulling in right direction
                    // so that player pulls the box to  right if  ctrl key is pressed with left arrow
                }
                // Push if on right & moving & velocity < 0 
                else if (!onLeftOfItem && anim.GetFloat("Speed") > .01 && thisRigidbody.velocity.x < 0)
                {
                    anim.SetBool("Pushing", true);
                    anim.SetBool("Pulling", false);
                }

                // if no speed or no velociyt i.e player is idle then do no aniamtion
                if (anim.GetFloat("Speed") < .01 || thisRigidbody.velocity.x == 0) {
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Pushing", false);
                }

            }       // ENDof r+Ctrl key if condition

            // if not holding Right control key then Push but no Pull animations

            //  Push if on left & no click on ctrl & moving & velocity > 0 
            // needed for pushing even not clicking on r+ctrl key
            if (onLeftOfItem && anim.GetFloat("Speed") > .01 && thisRigidbody.velocity.x > 0 )
            {
                anim.SetBool("Pushing", true);
                anim.SetBool("Pulling", false);
                noActionOnLeft = true;      // so that player moves left if no ctrl key is pressed with left arrow
            }
       
            // Push if on left and no ctrl key pressed
            if (onLeftOfItem && Input.GetKeyUp(KeyCode.RightControl)) {
                anim.SetBool("Pushing", true);
                anim.SetBool("Pulling", false);
                noActionOnLeft = true;      // so that player moves left if no ctrl key is pressed with left arrow
            }
            // Dont to show push animation when on left side and going left also dont show pull animation since ctrl key is not clicked
            if (onLeftOfItem && Input.GetKeyUp(KeyCode.RightControl) && thisRigidbody.velocity.x < 0 )
            {
                anim.SetBool("Pushing", false);
                anim.SetBool("Pulling", false);
                noActionOnLeft = true;    //  so that player moves left if no ctrl key is pressed with left arrow
            }

            // Push if on Right & moving & velocity < 0
            if (!onLeftOfItem && anim.GetFloat("Speed") > .01 && thisRigidbody.velocity.x < 0 )
            {
                anim.SetBool("Pushing", true);
                anim.SetBool("Pulling", false);
                noActionOnRight = true;             // flip to right since no need for pulling without clicking on ctrl key
            }
            // so as to push even if the ctrl key is not clicked
            if (!onLeftOfItem && Input.GetKeyUp(KeyCode.RightControl))
            {
                anim.SetBool("Pushing", true);
                anim.SetBool("Pulling", false);
                noActionOnRight = true;
            }
            // for no pushing and no pulling anim when player goes right.
            if (!onLeftOfItem && Input.GetKeyUp(KeyCode.RightControl) && thisRigidbody.velocity.x > 0)
            {
                anim.SetBool("Pushing", false);
                anim.SetBool("Pulling", false);
                noActionOnRight = true;
            }


            // if no speed or no velocity i.e player is idle then do no aniamtion
            if (anim.GetFloat("Speed") < .01 || thisRigidbody.velocity.x == 0)
            {
                anim.SetBool("Pulling", false);
                anim.SetBool("Pushing", false);
            }

        }       // END of main if
        else {

            // if not near box then can flip when needed;
            noActionOnLeft = !noActionOnLeft;
            noActionOnRight = !noActionOnRight;
            // no animation is shown when player is far from obj
            anim.SetBool( "Pushing", false );
            anim.SetBool( "Pulling", false );

            // resetting the player speed to the default value
            moveSpeed = currentPlayerSpeed;
            //anim.SetBool("onBox", false);
        }

/* Animation is not working for dead
        if (HealthManger.playerHealth <= 0) {

            anim.SetBool("Dead", true);
        }
 */

	}   // END OF UPDATE();



    public void LateUpdate() {
       // anim.SetBool("Dead", false);         
    }


    // Player Movement
    public void Move( float moveInput ) { 
        moveVelocity = moveSpeed * moveInput ;
    }

    // Player Firing
    public void Fire() {
        if (fireCounter <= 0) {
            Instantiate(laserBeamObj, firingPoint.transform.position, firingPoint.transform.rotation);
            fireCounter = fireDelay;
        }
    }

    // Player Jumping
    public void Jump() {
        //thisRigidBody.velocity = new Vector2( thisRigidBody.velocity.x, jumpHeight * Time.deltaTime );
        thisRigidbody.AddForce( new Vector2( thisRigidbody.velocity.x, jumpHeight ));
        
    }

    // Moving platform
    // for finding a collision with a moving platform and not to slide over by adusting both position relative to each other.
    public void OnCollisionEnter2D(Collision2D gameItem)
    {

        // if we have landed on the moving platform.                // Collision2D item have not tag thus transform.tag.....
        if (gameItem.transform.tag == "MovingGround")
        {
            // moving platform becomes  parent of the player thus moving the player along with the platform which is the parent where as player is child
            transform.parent = gameItem.transform;
        }
    }

    public void OnCollisionExit2D(Collision2D gameItem)
    {

        if (gameItem.transform.tag == "MovingGround")
        {

            // changing the parent of the transform to null. as there was not parent for the player before...
            transform.parent = null;
        }
    }
     

}   // END OF CLASS;
