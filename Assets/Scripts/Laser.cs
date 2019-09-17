using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public float beamSpeed;
    private PlayerController player;
    private Rigidbody2D thisRigidbody;
    public GameObject sparkObj;
    public int damageValue;
   // EnemyHealthManager enemyHealth;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerController>();
        thisRigidbody = GetComponent<Rigidbody2D>();
     //   enemyHealth = FindObjectOfType<EnemyHealthManager>();

        // Wont work if given in the Update as it is checked frequently beams wont go if player suddenly faces oppsotite direction
        // if player is facing left then the beam shoud go left
        if (player.transform.localScale.x < 0)
        {
            beamSpeed = -beamSpeed;

            // is for flipping the cone shaped laser as the player flips.
            thisRigidbody.transform.localScale = new Vector3(-1.5f, 1.2f, 1f);
            // ths size is -1.5f, 1.2f since i have scaled the size of the laser to be larger which needs to be same even it is flipped
        }

	}       // END OF START();

	
	// Update is called once per frame
	void Update () {

     

        // setting the beam firing speed and transform;
        thisRigidbody.velocity = new Vector2( beamSpeed * Time.deltaTime, thisRigidbody.velocity.y );
        
	}       // END OF UPDATE();


    // on colliding with something
    public void OnTriggerEnter2D( Collider2D gameItem ) {

        // on hitting an enemy
        if (gameItem.tag == "Enemy") {

            //EnemyHealthManager.DamageEnemy(damageValue);
           // enemyHealth.DamageEnemy(damageValue);
            gameItem.GetComponent<EnemyHealthManager>().DamageEnemy(damageValue);
            
        }

        // when hittint the Boss Enemy
        if (gameItem.tag == "BossEnemy") {

            // when fighting with the boss enemy
            gameItem.GetComponent<BossHealthManager>().DamageEnemy(damageValue);
        }

        Instantiate( sparkObj, transform.position, transform.rotation );
        Destroy(gameObject);        // destroy this game object up on collision
    } 

}       // END OF CLASS;
