using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

    public float moveSmoothness;
    public float slowPlayerSpeed;
    Rigidbody2D thisRigidbody;
    PlayerController player;
    public Transform moveWith;              // the item near check game obj of the player not the player positin.
    Vector3 offset;
    Vector3 target;
    public float xOffset;
  
   
	// Use this for initialization
	void Start () {
	
        thisRigidbody = GetComponent<Rigidbody2D>();
    
        player = FindObjectOfType<PlayerController>();
        offset = new Vector3( xOffset, 0, 0 );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerAction() {

        // slowing the player down so as to not to lose the box while pulling and to show box weight (realism)
        player.moveSpeed = slowPlayerSpeed;

        // so that if the player is on the right or left side there will not be any problms
        // giving moveWithposition + offset will give error when player moves to the right side of the box        

        if (player.transform.position.x < transform.position.x) {
            target = moveWith.position + offset;
        }
        else {
             target = moveWith.position - offset;
        }
     

        // chnaging the position of the object with the player
        //Animator anim =  player.GetComponent<Animator>();
        
        transform.position = Vector3.Lerp(transform.position, target, moveSmoothness * Time.deltaTime);
    }
}
