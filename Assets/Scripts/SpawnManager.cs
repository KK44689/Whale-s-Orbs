using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // fish prefabs
    public GameObject orbPrefab;

    // ship prefabs
    public GameObject[] shipPrefab;

    public int maxShipIndex = 0;

    // coral prefabs
    public GameObject[] coralPrefab;

    // boundary
    public GameObject leftWall;

    public GameObject rightWall;

    public GameObject topWall;

    public GameObject bottomWall;

    // orbs spawn times
    // public float orbSpawnRate = 3f;
    public int maxOrbSpawnRate = 3;

    // orbs count
    private int orbCount = 0;

    // ships count
    public int shipsCount = 0;

    // coral count
    public int coralCount = 0;

    //ships spawn rate
    public int maxShipSpawnRate = 1;

    // coral spawn
    public int coralSpawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCoral (coralSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        // spawn fishes
        orbCount = FindObjectsOfType<Orb>().Length;
        if (orbCount == 0)
        {
            int orbsSpawn = Random.Range(0, maxOrbSpawnRate + 1);

            // int fishSpawn = Mathf.CeilToInt(Random.Range(0, maxOrbSpawnRate));
            SpawnOrbs (orbsSpawn);
        }

        //spawn ships
        shipsCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (shipsCount == 0)
        {
            int shipSpawn = Random.Range(1, maxShipSpawnRate);
            SpawnShips (shipSpawn);
        }

        //spawn corals
        coralCount = GameObject.FindGameObjectsWithTag("Coral").Length;
        if (coralCount == 0)
        {
            SpawnCoral (coralSpawn);
        }
    }

    void SpawnOrbs(int orbsSpawn)
    {
        for (int i = 0; i < orbsSpawn; i++)
        {
            Instantiate(orbPrefab,
            GenerateSpawnPos(0),
            orbPrefab.transform.rotation);
        }
    }

    public void SpawnShips(int shipsSpawn)
    {
        for (int i = 0; i < shipsSpawn; i++)
        {
            int index = Random.Range(0, maxShipIndex + 1);
            Instantiate(shipPrefab[index],
            GenerateSpawnPos(4),
            shipPrefab[index].transform.rotation);
        }
    }

    public void SpawnCoral(int coralSpawn)
    {
        for (int i = 0; i < coralSpawn; i++)
        {
            int index = Random.Range(0, 2);
            Instantiate(coralPrefab[index],
            GenerateSpawnPos(0),
            coralPrefab[index].transform.rotation);
        }
    }

    Vector3 GenerateSpawnPos(float yOffset)
    {
        float randX =
            Random
                .Range(leftWall.transform.position.x + 2f,
                rightWall.transform.position.x - 2f);
        float randZ =
            Random
                .Range(bottomWall.transform.position.x + 2f,
                topWall.transform.position.x - 2f);
        Vector3 spawnPos = new Vector3(randX, 0.4f + yOffset, randZ);
        return spawnPos;
    }
}