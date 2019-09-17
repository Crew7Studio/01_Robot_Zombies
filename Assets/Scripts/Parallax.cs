using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    public Transform[] backgrounds;
    public float[] parallaxScales;          // how much bk should move relative to camera
    public float smoothing;

    private Transform cam;
    private Vector3 previousCamPos;

	// Use this for initialization
	void Start () {
        //smoothing = .9f;

        cam = Camera.main.transform;
        previousCamPos = cam.position;

        parallaxScales = new float[ backgrounds.Length ];       // for setting parallax scales for amount of bks

        for (int i = 0; i < backgrounds.Length; i++) {

            parallaxScales[i] = backgrounds[i].position.z * -1;         // making the +ve z value to -ve z value
        }
	
	}
	
	// Late Update is called after Update in each frame.
	void LateUpdate () {            // so that this is executed last......// we also have camera script which may cause error.


        for (int i = 0; i < backgrounds.Length; i++) {

            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];           // gets the amount of movement

            float backgroundTargetPositionX = backgrounds[i].position.x + parallax;             // parralas is  a-ve value thus changing the bk pos backwarks adding a -ve value based on camera movement

            Vector3 backgroundTargertPos = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Vector3.Lerp( current pos, target pos, float value for smoothing) is for smoot transition. i.e changing.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargertPos, smoothing * Time.deltaTime);
        }

        // resetting the cam position
        previousCamPos = cam.position;
	}
}
