// checkpoint is used for finding the positin for respwaning the player.

using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    LevelManager levelMngr;         // level manager is used for respwaning the player 

	// Use this for initialization
	void Start () {

        levelMngr = FindObjectOfType<LevelManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // on entering a checkpoint i.e on trigerring a chekc point
    public void OnTriggerEnter2D(Collider2D gameItem) {

        if (gameItem.tag == "Player") {

            // setting this checkpoint gameobject as the checkpoint object in the lavel manager so that player could respwan in this gameobject position
            levelMngr.checkpointPos = gameObject;
        }
    }
}
