using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public static int maxLife;
    public static int lifeCount;

    public Text text;

    PlayerController player;
    public GameObject gameOverScreen;
    public string mainMenuScene;
    public float delayAfterGameOver;

	// Use this for initialization
	void Start () {

        maxLife = PlayerPrefs.GetInt("playerLife");
        lifeCount = maxLife;
        text = GetComponent<Text>();

        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        // to display the number of life left
        text.text = " x  " + lifeCount;

        if (lifeCount <= 0) {

            // GameOver
            gameOverScreen.SetActive(true);
            //Debug.Log("gameover");
        }
        else {
            gameOverScreen.SetActive(false);
        }

        if (gameOverScreen.activeSelf) {

            // so that the player wont move 
            player.enabled = false;
            delayAfterGameOver -= Time.deltaTime;
        }

        if (delayAfterGameOver <= 0) {
            // for loading a new scene here the main menu
            Application.LoadLevel(mainMenuScene);
        }

     
	}

    // increase life if player gets new life.
    public static void GiveLife() {

   //     if (lifeCount >= maxLife) {
      //      lifeCount = maxLife;
     //   }
       // else {
            lifeCount++;
            // so that the life of the player increases accross all levels if he successfully exits from the level
            PlayerPrefs.SetInt("playerLife", lifeCount);
        //}
    }

    // decrease life if health == 0;
    public static void TakeLIfe() {

        lifeCount--;
        // so that the life of the player decreases accross all levels if he successfully exits from the level
        PlayerPrefs.SetInt("playerLife", lifeCount);

    }
}
