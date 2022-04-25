using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
    // player script
    private PlayerController playerScript;

    // spawn manager script
    private SpawnManager spawnManagerscript;

    //player
    GameObject player;

    // exp bar
    public ExpBar expBar;

    // hp bar
    public HpBar hpBar;

    // player scale
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        //get player script
        playerScript =
            GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        //get spawn manager script
        spawnManagerscript =
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        // get player
        player = GameObject.FindWithTag("Player");

        // set scale that will change when go to next level
        scaleChange = new Vector3(0.2f, 0.2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.exp == playerScript.maxExp)
        {
            // set new max exp
            SetNewMaxExp();

            // set new max hp
            SetNewMaxHp();

            // set player scale
            SetPlayerScale();

            // set player speed
            SetPlayerSpeed();

            // add new ship type when max exp == 10 & max exp == 20
            AddNewShip();

            // add coral
            AddCoral();

            // add ships
            AddMoreShip();

            // add more orbs
            AddMoreOrbs();
        }
    }

    void SetNewMaxExp()
    {
        playerScript.exp = 0;
        playerScript.maxExp += 5;
        expBar.SetExp(playerScript.exp);
        expBar.SetMaxExp(playerScript.maxExp);
        Debug.Log("max exp : " + playerScript.maxExp);
    }

    void SetNewMaxHp()
    {
        playerScript.maxHp += 5;
        playerScript.hp = playerScript.maxHp;
        hpBar.SetHp(playerScript.hp);
        hpBar.SetMaxHp(playerScript.maxHp);
        Debug.Log("max hp : " + playerScript.maxHp);
    }

    void SetPlayerScale()
    {
        if (!Mathf.Approximately(player.transform.localScale.x, 2f))
        {
            player.transform.localScale += scaleChange;
        }
    }

    void SetPlayerSpeed()
    {
        playerScript.speed += 2;
    }

    void AddNewShip()
    {
        if (playerScript.maxExp >= 10)
        {
            spawnManagerscript.maxShipIndex = 1;
            Debug.Log("add super sail");
        }
        if (playerScript.maxExp >= 20)
        {
            spawnManagerscript.maxShipIndex = 2;
            Debug.Log("add black pearl");
        }
    }

    void AddCoral()
    {
        spawnManagerscript.SpawnCoral(1);
    }

    void AddMoreShip()
    {
        if (playerScript.maxExp >= 20 && (playerScript.maxExp % 2) == 0)
        {
            spawnManagerscript.maxShipSpawnRate++;
            spawnManagerscript.SpawnShips(1);

            // Debug.Log("ship count : " + spawnManagerscript.shipsCount);
            // Debug.Log("max spawn ship : " + spawnManagerscript.maxShipSpawnRate);
        }
    }

    void AddMoreOrbs()
    {
        if (playerScript.maxExp >= 20 && (playerScript.maxExp % 10) == 0)
        {
            spawnManagerscript.maxOrbSpawnRate++;
            Debug.Log("orbs spawn : " + spawnManagerscript.maxOrbSpawnRate);
        }
    }
}
