using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform targetPlace;         // i.e target is the player
    Vector3 offset;                               // distance between camera and the player pos is taken as the constant offset distance between them at all times

    public float smoothing;         // value used for smoothing camera movvement relative to player
    float lowY;                 // the lowest point in y that camera can follow the player. i.e when the player falls down camera follows up to a point only
 
    void Start() {

        offset = transform.position - targetPlace.position;          // currently placed distance between camera and the player is taken as the offset
        lowY = transform.position.y - 1;           /// lowest point until the camera follows the player
    }

    // since player movement is based on the fixed update we use this for setting up the camera movement too.
    void FixedUpdate() {

        // the camera should follow always the player position with an offset value.
        Vector3 targetCamPos = targetPlace.position + offset;

        // Vector3.Lerp( currentPos, targetPos, smoothening value );    is used to smoothly transfomr the position of something.
        // changing the camera position smoothly
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.y < lowY)
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
    }
}
