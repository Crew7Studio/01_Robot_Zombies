using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public string nextLevel;

    public string levelTag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D gameItem) {

        if (gameItem.tag == "Player") {

            Application.LoadLevel(nextLevel);
            PlayerPrefs.SetInt(levelTag, 1);
        }
    }   
}
