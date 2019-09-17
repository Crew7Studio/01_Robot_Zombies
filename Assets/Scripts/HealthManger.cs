using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManger : MonoBehaviour
{
    public int givenHealth;
    public static int maxPlayerHealth;
    public static int playerHealth;

    LevelManager levelMngr;
    LifeManager life;

    public Slider healthBar;

    // image to show while damaged
    public Image damageScreen;                              // canvas image to be shown
    Color color = new Color(0f, 0f, 0f, .8f);
     bool damaged;
     float colorChangeSmoothingValue = 2f;

     PlayerController player;

     float delayCounter;
    // Use this for initialization
    void Start()
    {

        maxPlayerHealth = givenHealth;
        playerHealth = maxPlayerHealth;
        life = FindObjectOfType<LifeManager>();
        levelMngr = FindObjectOfType<LevelManager>();

        player = FindObjectOfType<PlayerController>();
        // if this script is not put in slider ( if in player) this cause error so put this script in player and drag slider obj to it;
        //healthBar = GetComponent<Slider>();
        damaged = false;

        delayCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.value = playerHealth;
          
        // health = o then player dies
        if (playerHealth <= 0 && delayCounter <= 0)
        {
            LifeManager.TakeLIfe();
            // resetting the health after the death of player.
            ResetHealth();              
            // if reset is not given here then player health = 0 so it would call respwan() several time in a second
            // causeing particles to go hay wire.
            // resetting health in levelmanager .respwan() wont work
            levelMngr.Respawn();

            // below code is used bcos since it is in update() the player gets killed several time thus several life is lost just in one hit or fall e.t.c
            delayCounter = levelMngr.respwanDelayTime + 1;
        }

        delayCounter -= Time.deltaTime;

        // if player damaged then increase the alpha of the image to make it visible using the color object 
        if (damaged) {
            damageScreen.color = color;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else {
            // if not damaged smoothley clear the color of the damage screedn i.e make alpha = null
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, colorChangeSmoothingValue * Time.deltaTime);

        }
        damaged = false;

    }       // END OF UPDATE
     
    // add health to the player
    public static void AddHealth()  {

        // so that player health wont increase more that the max value he have
        if (playerHealth >= maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
        else
            playerHealth++;
    }       // END OF ADDHEALTH();      


    // Remove health from player
    public static void Damage(int damageValue) {

        playerHealth -= damageValue;
        FindObjectOfType<HealthManger>().damaged = true;
      
    }  // END OF DAMAGE();


    // resetitng the player health to max on respwaning
    public static void ResetHealth() {
    
        playerHealth = maxPlayerHealth;         // reset the health to max
    }
}       // END OF CLASS;    
