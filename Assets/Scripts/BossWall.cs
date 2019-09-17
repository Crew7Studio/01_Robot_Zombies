using UnityEngine;
using System.Collections;

public class BossWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // check if any boss health manager is presenet i.e any boss enemy if present if yes then it simply returns  if not it destroys
        if (FindObjectOfType<BossHealthManager>()) {
            return;
        }

        // destroys the obj attached to it
        Destroy(gameObject);
	}
}
