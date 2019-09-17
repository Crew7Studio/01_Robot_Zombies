using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {


    public GameObject saw;
    AudioClip clip;

    // Use this for initialization
	void Start () {

      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // if something hits with the saw
    public void OnTriggerEnter2D(Collider2D gameItem) {

        //if that someone is the player
        if (gameItem.tag == "Player") {
            saw.GetComponent<AudioSource>().PlayOneShot(clip, 1);
        
        }
    }

}
