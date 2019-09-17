using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    Text text;
    public float maxTime;
     public float currentTime;          // public so that clock.cs can access it

    public GameObject timesUpScreen;
    public float fadeDelay;
    float fadeDelayCounter;

	// Use this for initialization
	void Start () {

        fadeDelayCounter = fadeDelay;

        text = GetComponent<Text>();
        currentTime = maxTime;
	}
	
	// Update is called once per frame
	void Update () {

        text.text = "Time  " + Mathf.Round(currentTime);

        currentTime -= Time.deltaTime;

        if (currentTime <= 0) {

            LifeManager.TakeLIfe();
            currentTime = maxTime;
        }

        // if time <= 10 then go fast text is shown with a shaded img as bk
        if (currentTime <= 10) {
            timesUpScreen.SetActive(true);
        }
        else
            timesUpScreen.SetActive(false);
        
	}
}
