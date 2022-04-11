using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // declare player's speed
    public float speed = 10f;

    // rigidbody variables
    Rigidbody playerRb;

    // player's input variables
    float horizontalInput;

    float verticalInput;

    Vector3 direction;

    // create your directions
    private static Quaternion forwardDirection = Quaternion.Euler(-90, 0, 0);

    private static Quaternion rightDirection = Quaternion.Euler(-90, 90, 0);

    private static Quaternion leftDirection = Quaternion.Euler(-90, -90, 0);

    private static Quaternion backDirection = Quaternion.Euler(-90, 0, 180);

    // enemy
    private GameObject enemyPrefab;

    // enemy force
    public float collideForce = 2f;

    // check collide with enemy
    private bool enemyCollided = false;

    // player exp
    public int maxExp = 5;

    public int exp;

    public ExpBar expBar;

    // Start is called before the first frame update
    void Start()
    {
        // get player's rigidbody
        playerRb = GetComponent<Rigidbody>();

        // set start exp
        expBar.SetMaxExp (maxExp);
        exp = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        // get player's input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(direction * speed * Time.deltaTime);
        // set direction left / right
        if (horizontalInput > 0)
        {
            transform.rotation = rightDirection;

            // // player move
            transform
                .Translate(Vector3.down *
                Time.deltaTime *
                speed *
                horizontalInput);
        }
        else if (horizontalInput < 0)
        {
            // direction = new Vector3(-1, 0, 0);
            transform.rotation = leftDirection;

            // // player move
            transform
                .Translate(Vector3.up *
                Time.deltaTime *
                speed *
                horizontalInput);
        } // set direction forward/backward
        else if (verticalInput > 0)
        {
            // direction = new Vector3(0, 1, 0);
            transform.rotation = forwardDirection;

            // // player move
            transform
                .Translate(Vector3.down *
                Time.deltaTime *
                speed *
                verticalInput);
        }
        else if (verticalInput < 0)
        {
            // direction = new Vector3(0, -1, -0);
            transform.rotation = backDirection;

            // // player move
            transform
                .Translate(Vector3.up * Time.deltaTime * speed * verticalInput);
        }

        // check collide with enemy
        // if (enemyCollided)
        // {
        //     Debug.Log("enemy");
        //     enemyPrefab = GameObject.FindWithTag("Enemy");
        //     Vector3 awayFromEnemy =
        //         (transform.position - enemyPrefab.transform.position)
        //             .normalized;
        //     playerRb.AddForce(awayFromEnemy * collideForce, ForceMode.Impulse);
        //     enemyCollided = false;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Orb"))
        {
            Destroy(other.gameObject);
            exp++;
            expBar.SetExp (exp);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // enemyCollided = true;
            Debug.Log("enemy");
            exp--;
            expBar.SetExp (exp);
            enemyPrefab = GameObject.FindWithTag("Enemy");
            Vector3 awayFromEnemy =
                (transform.position - enemyPrefab.transform.position)
                    .normalized;
            playerRb.AddForce(awayFromEnemy * collideForce, ForceMode.Impulse);
            // enemyCollided = false;
        }
    }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         enemyCollided = false;
    //     }
    // }
}
