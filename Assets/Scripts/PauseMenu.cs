using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public string mainMenu;
    public string levelSelect;

    public GameObject pauseMenuCanvas;
    public bool isPaused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if( Input.GetKeyDown( KeyCode.Escape )) {
            
            pauseUnPause();
        }

        // if paused then
        if( isPaused ) {
            pauseMenuCanvas.SetActive( true );
            Time.timeScale = 0;
        }
        else {      // if unpaused then
            pauseMenuCanvas.SetActive( false );
            Time.timeScale = 1;
        }

	}   // End of UPdate

    // while clicking on the esc key to pause or unpause the game
    public void pauseUnPause() {
        isPaused = !isPaused;
    }

    public void Resume() {
        
        isPaused = false;
    }

    public void MainMenu() {
        
        Application.LoadLevel( mainMenu );
    }

    public void LevelSelect() {
        
        Application.LoadLevel( levelSelect );
    }

    public void Quit() {
        
        Application.Quit();
    }
}
