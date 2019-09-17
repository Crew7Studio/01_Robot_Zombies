using UnityEngine;
using System.Collections;

public class ShootPlayerInRange : MonoBehaviour
{

    public float radarBoundary;
    public GameObject zombieLaser;
    public Transform pointOfFiring;
    public PlayerController player;

    public float waitingTimeToShoot;
    public float counter;

    // Use this for initialization
    void Start()
    {

        player = FindObjectOfType<PlayerController>();
        // waitingTimeToShoot = 1;
        counter = waitingTimeToShoot;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(new Vector3(transform.position.x - radarBoundary, transform.position.y, transform.position.z), new Vector3(transform.position.x + radarBoundary, transform.position.y, transform.position.z));

        counter -= Time.deltaTime;

        // for firing right side
        // if enemy moving right && playher is on right of enemy && player positin with in range of the enemy
        if (transform.localScale.x > 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + radarBoundary && counter <= 0)
        {
            Instantiate(zombieLaser, pointOfFiring.position, pointOfFiring.rotation);
            counter = waitingTimeToShoot;
        }

        // for firing left side
        if (transform.localScale.x < 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - radarBoundary && counter <= 0)
        {
            Instantiate(zombieLaser, pointOfFiring.position, pointOfFiring.rotation);
            counter = waitingTimeToShoot;

        }
    }
}
