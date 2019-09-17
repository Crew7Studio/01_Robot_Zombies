using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {

    public AudioSource audio;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D gameItem) {

        if (gameItem.tag == "Player") {

            HealthManger.AddHealth();
            audio.Play();
        }
        Destroy(gameObject);
    }
}
