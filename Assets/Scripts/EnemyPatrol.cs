using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed;
    public bool moveRight;

    Rigidbody2D enemy;

    // for checking if wall is hit
    public Transform wallChecker;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool walled;

    // for checking the edge of the ground
    public Transform edgeChecker;
    private bool atEdge;

    EnemyHealthManager enemyHealthMngr;

    // Use this for initialization
    void Start()
    {

    //    moveSpeed = 100;
        //moveRight = false;
        enemy = GetComponent<Rigidbody2D>();
        walled = false;
        wallCheckRadius = .1f;
        enemyHealthMngr = FindObjectOfType<EnemyHealthManager>();

    }


    // Update is called once per frame
    void Update()
    {

        // checking if enemy hitted a wall
        walled = Physics2D.OverlapCircle(wallChecker.position, wallCheckRadius, whatIsWall);            // giving it in FixedUPdate didn't worked.

        // checking if ground is beneath enemy
        atEdge = Physics2D.OverlapCircle(edgeChecker.position, wallCheckRadius, whatIsWall);

        // enemy fist moves to left with the wall checker on its left.

        if (walled || !atEdge)
        {
            moveRight = !moveRight;         // go to the opposite direction
        }

        if (moveRight)
        {         // moving right
            enemy.velocity = new Vector2(moveSpeed * Time.deltaTime, enemy.velocity.y);
            enemy.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            // moving left
            enemy.velocity = new Vector2(-moveSpeed * Time.deltaTime, enemy.velocity.y);
            enemy.transform.localScale = new Vector3(-1f, 1f, 1f);

        }

    }       // End of Update();

    void FixedUpdate() {

     //   if (enemyHealthMngr.enemyHealth <= 0) Destroy(gameObject);
    }
}
