using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

    public float timeBonus;
    TimeManager time;
    public AudioSource audio;
	// Use this for initialization
	void Start () {
        time = FindObjectOfType<TimeManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D gameItem) {
       
        if (gameItem.tag == "Player") {
            audio.Play();
            time.currentTime += timeBonus;
          
        }
        Destroy(gameObject);
    }
}
