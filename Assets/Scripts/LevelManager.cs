using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public float respwanDelayTime;
    public GameObject checkpointPos;
    PlayerController player;

    public GameObject deathParticleObj;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // function for respwaning the player after the death
    public void Respawn() {

        // making a coroutine
        StartCoroutine("RespawnDelay");
    }

    // here IEnumerator has no retuen type
    public IEnumerator RespawnDelay() {

    // shows death particles
    Instantiate(deathParticleObj, player.transform.position, player.transform.rotation);

    player.enabled = false;         // so that user have no control over player after death ;   
    player.GetComponent<Renderer>().enabled = false;                // so that player is not shown after death
    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;         // setting velocity to 0 so that camera wont move with the player ...

    yield return new WaitForSeconds( respwanDelayTime );

    // re enabling player features
    player.enabled = true;
    player.GetComponent<Renderer>().enabled = true;

    // player is moved to the checkpoint gameobjects position.  which is changed as player passes through each chekcpoint
    player.transform.position = checkpointPos.transform.position;
   
    // Spwan particle

    // resets player healht on respwaning
    //  even though we are resetting player health in update() of helath manager, 
    //when player respwans sometime if he is taking continous damage at respwaning full health is not given
    //so we are resetting the helth here again
    HealthManger.ResetHealth();
    }
}
