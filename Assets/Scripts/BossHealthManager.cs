using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public int enemyHealth;

    public GameObject deathParticle;
    //  ZombiePatrol zombieEnemy;

    Rigidbody2D thisRigidbody;

    public Slider enemyHealthSlider;

    public GameObject bossPrefab;
    public float bossMinSize;

    // Use this for initialization
    void Start()
    {

        //  zombieEnemy = FindObjectOfType<ZombiePatrol>();
        thisRigidbody = GetComponent<Rigidbody2D>();

        //enemyHealthSlider = GetComponent<Slider>();
        enemyHealthSlider.maxValue = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {

        enemyHealthSlider.value = enemyHealth;

        // if health of enemy is 0 then kill enemy
        if (enemyHealth <= 0)
        {

            Instantiate(deathParticle, transform.position, transform.rotation);
            //  Destroy(zombieEnemy);

            // if boss enemy height is greater than min size then he can split i.e make clones of itself;
            if (transform.localScale.y > bossMinSize) {

                // making 2 clones of the boss enemy
                GameObject clone1 = Instantiate(bossPrefab, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                GameObject clone2 = Instantiate(bossPrefab, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
                GameObject clone3 = Instantiate(bossPrefab, new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), transform.rotation) as GameObject;

                // declreaseing the size of each clones by half         // here we need same value for x and y so we use y both time
                clone1.transform.localScale = new Vector3(transform.localScale.y * .5f, transform.localScale.y * .5f, transform.localScale.z);
                // giving health to each clones
                clone1.GetComponent<BossHealthManager>().enemyHealth = 10;
                clone2.transform.localScale = new Vector3(transform.localScale.y * .5f, transform.localScale.y * .5f, transform.localScale.z);
                clone2.GetComponent<BossHealthManager>().enemyHealth = 10;
                clone3.transform.localScale = new Vector3(transform.localScale.y * .5f, transform.localScale.y * .5f, transform.localScale.z);
                clone3.GetComponent<BossHealthManager>().enemyHealth = 10;
            }

            Destroy(gameObject);
        }
    }   // END of UPdate();

    void FixedUpdate()
    {

        // so that if the enemy flips the slider flips too so as tol look consistent
        if (thisRigidbody.transform.localScale.x < 0)
        {
            enemyHealthSlider.direction = Slider.Direction.RightToLeft;
        }
        else enemyHealthSlider.direction = Slider.Direction.LeftToRight;

    }

    public void DamageEnemy(int damageValue)
    {
        // by default we unchecked the enemy health slider so it is not seen until the enemy is hit for the first time.
        // we are turning back or checking the enemy health slider back on 
        enemyHealthSlider.gameObject.SetActive(true);
        enemyHealth -= damageValue;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
