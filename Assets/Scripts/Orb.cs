using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    // movement points
    public float moveRange = 5;

    private Vector3 endPoint;

    private Vector3 startPoint;

    private float randX;

    private float randZ;

    // speed
    private float speed = 0.5f;

    // player script
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        RandomMove();
        playerScript =
            GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void RandomMove()
    {
        // random end point
        randX = Random.Range(-moveRange, moveRange);
        randZ = Random.Range(-moveRange, moveRange);
        startPoint = transform.position;
        endPoint = new Vector3(randX, 0.4f, randZ);

        // random speed
        speed = Random.Range(0.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // move orbs back & forth
        if (playerScript.isGameActive)
        {
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(startPoint, endPoint, time);
        }
    }
}
