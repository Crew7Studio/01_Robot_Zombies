using UnityEngine;
using System.Collections;

public class DestroyObjecOverTime : MonoBehaviour {

    public float aliveTime;

	// Use this for initialization
	void Start () {
        // destroys the gameobj that this script is attached to after a certain amount of time specified;
        Destroy(gameObject, aliveTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
