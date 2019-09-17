using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour {

    public float moveSpeed;
    Rigidbody2D thisRigidbody;
    Animator anim;
    public Transform firingPoint;

	// Use this for initialization
	void Start () {

        thisRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        // flips the drone in the correct direction
        if (thisRigidbody.velocity.x < 0)
        {
            thisRigidbody.transform.localScale = new Vector3(-.6f, .6f, 1f);
        }
        if (thisRigidbody.velocity.x > 0)
        {
            thisRigidbody.transform.localScale = new Vector3(.6f, .6f, 1f);
        }

    }       // END of FixedUpdate();

    // Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.J)) {

            thisRigidbody.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.L)) {
            thisRigidbody.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
        }

        anim.SetFloat("DroneSpeed", Mathf.Abs(thisRigidbody.velocity.x));

	}       // END of Update();

}   // END of class
