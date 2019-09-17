using UnityEngine;
using System.Collections;

public class AddLIfe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D gameItem) {

        if (gameItem.tag == "Player") {

            LifeManager.GiveLife();
        }
        Destroy(gameObject);
    }
}
