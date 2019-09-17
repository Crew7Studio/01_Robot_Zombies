using UnityEngine;
using System.Collections;

public class RotatingStrip : MonoBehaviour {

  
    int angleIncrement;

    Rigidbody2D thisRigidbody;

	// Use this for initialization
	void Start () {

        thisRigidbody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        // rotates the rigid body to the specified angle
        thisRigidbody.MoveRotation(angleIncrement);
        angleIncrement++;
	}
}
