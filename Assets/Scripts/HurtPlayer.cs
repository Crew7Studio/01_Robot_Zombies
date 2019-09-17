// code added to enemies or other things which hurts player on contack

using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

    private PlayerController player;
    public int damageValue;

	
    // Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerController>();
      

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D gameItem) {

        // if player hits me then 
        if (gameItem.tag == "Player") { 
        
            // decreasing the health of the player;
            HealthManger.Damage(damageValue);


            // code for knocking back the player
            player.knockbackCounter = player.knockbackDelay;

            // if player is knocked from the right side then 
            if (gameItem.transform.position.x < transform.position.x) {
                player.rightKnock = true;
            }
            else {
                player.rightKnock = false;
            }

        }
    }
}
