using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string startingLevel;
    public string levelSelect;
    public int lifeOfPlayer;

    public string level1Tag;
    public string[] levelTags;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame() {

        Application.LoadLevel(startingLevel);
        PlayerPrefs.SetInt(level1Tag, 1);

        for (int i = 0; i < levelTags.Length; i++) {

            PlayerPrefs.SetInt(levelTags[i], 0);
        }

            // setting a constant life for player on starting game every time
            PlayerPrefs.SetInt("playerLife", lifeOfPlayer);
    }

    public void LevelSelect() {

        Application.LoadLevel(levelSelect);
        PlayerPrefs.SetInt(level1Tag, 1);
        // setting  a constant life for player for every level.
        PlayerPrefs.SetInt("playerLife", lifeOfPlayer);
        // if we dont set this if a player loses  i.e game over where life = 0 and if player chooses to selct level from main menu he wont be able to play since life = 0

    }

    public void Quit() {

        Application.Quit();
    }
}
