using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    private Rigidbody enemyRb;

    public float speed = 10f;

    // player script
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyRb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isGameActive)
        {
            Vector3 lookDirection =
                (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
        else
        {
            enemyRb.velocity = Vector3.zero;
        }
    }
}
