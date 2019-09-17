using UnityEngine;
using System.Collections;

public class ZombieLaser : MonoBehaviour {


    public float beamSpeed;
    public int damageValue;
    public GameObject sparkObj;
    private Rigidbody2D thisRigidbody;

  //  private ZombiePatrol zombieEnemy;
    PlayerController player;

	// Use this for initialization
	void Start () {
	
        thisRigidbody = GetComponent<Rigidbody2D>();
       // zombieEnemy = FindObjectOfType<ZombiePatrol>();
        player = FindObjectOfType<PlayerController>();

        // if zombie is moving left flip the laser to the left side too.
 /*       if (zombieEnemy.transform.localScale.x < 0)
        {

            // so that the beam move in -ve direction i.e left side
            beamSpeed = -beamSpeed;
            // flips the laser. (laser size increased thus floating valus here)
            thisRigidbody.transform.localScale = new Vector3(-1.5f, 1.2f, 1f);
        }
       
        */
        if (player.transform.position.x < transform.position.x) {

            beamSpeed = -beamSpeed;
            // flips the laser. (laser size increased thus floating valus here)
            thisRigidbody.transform.localScale = new Vector3(-1.5f, 1.2f, 1f);
        }
	}       // END OF START();
	
	// Update is called once per frame
	void FixedUpdate () {

        thisRigidbody.velocity = new Vector2(beamSpeed * Time.deltaTime, thisRigidbody.velocity.y);
	}   // END OF UPDATE()

    public void OnTriggerEnter2D(Collider2D gameItem) {

        // if player is hit by laser then
        if (gameItem.tag == "Player") {

            HealthManger.Damage(damageValue);
        }
        // for sparks
        Instantiate(sparkObj, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}   // END OF CLASS
