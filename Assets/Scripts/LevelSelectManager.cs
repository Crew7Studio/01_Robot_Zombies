using UnityEngine;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {

    public string[] levelTags;
    public GameObject[] locks;
    public bool[] levelUnlock;

    public int positionSelector;
    public float xOffset;
    public float yOffset;
    public string[] levelName;
    public float moveSpeed;
    public bool isPressed;


	// Use this for initialization
	void Start () {

        for (int i = 0; i < levelTags.Length; i++) {

            if (PlayerPrefs.GetInt(levelTags[i]) == null) {

                levelUnlock[i] = false;
            }
            else if (PlayerPrefs.GetInt(levelTags[i]) == 0) {

                levelUnlock[i] = false;
            }
            else {
                levelUnlock[i] = true;
            }

            if (levelUnlock[i]) {
                locks[i].SetActive(false);
            }

            transform.position = locks[positionSelector].transform.position + new Vector3(xOffset, yOffset, 0);
        }
        isPressed = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isPressed) {

            if (Input.GetAxis("Horizontal") > 0.25f) {        // if player pressing right arrow
                positionSelector += 1;
                isPressed = true;
            }

            if (Input.GetAxis("Horizontal") < -0.25f){        // if player pressing left arrow
                positionSelector -= 1;
                isPressed = true;
            }

            if (positionSelector >= levelTags.Length) {
                positionSelector = levelTags.Length - 1;
            }

            if (positionSelector < 0) {
                positionSelector = 0;
            }

        }

        if (isPressed) {
            if (Input.GetAxis("Horizontal") < 0.25f && Input.GetAxis("Horizontal") > -0.25f) {
                isPressed = false;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, locks[positionSelector].transform.position + new Vector3(xOffset, yOffset, 0), moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit")) {

            if (levelUnlock[positionSelector]) {
                Application.LoadLevel(levelName[positionSelector]);
            }
        }
	}
}
