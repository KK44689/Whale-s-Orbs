using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // orbs prefabs
    public GameObject orbPrefab;

    // red orb prefabs
    public GameObject redOrbPrefab;

    // ship prefabs
    public GameObject[] shipPrefab;

    private int m_maxShipIndex = 0;

    public int maxShipIndex
    {
        get
        {
            return m_maxShipIndex;
        }
        set
        {
            if (value > 2)
            {
                Debug
                    .LogError("ship's index is up to 2 (have three ship types)");
            }
            else
            {
                m_maxShipIndex = value;
            }
        }
    }

    // coral prefabs
    public GameObject[] coralPrefab;

    // boundary
    public GameObject leftWall;

    public GameObject rightWall;

    public GameObject topWall;

    public GameObject bottomWall;

    // orbs spawn times
    private int m_maxOrbSpawnRate = 3;

    public int maxOrbSpawnRate
    {
        get
        {
            return m_maxOrbSpawnRate;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("max Orb spawn rate can't be negative!");
            }
            else
            {
                m_maxOrbSpawnRate = value;
            }
        }
    }

    // orbs count
    private int orbCount = 0;

    // red orbs spawn rate
    private float redOrbSpawnRate;

    // red orbs count
    private int redOrbCount = 0;

    private float minRedOrbSpawnRate = 15;

    private float maxRedOrbSpawnRate = 30;

    // ships count
    [SerializeField]
    private int shipsCount = 0;

    // coral count
    [SerializeField]
    private int coralCount = 0;

    //ships spawn rate
    private int m_maxShipSpawnRate = 1;

    public int maxShipSpawnRate
    {
        get
        {
            return m_maxShipSpawnRate;
        }
        set
        {
            if (value < 1)
            {
                Debug.LogError("ships number can't lower than one!");
            }
            else
            {
                m_maxShipSpawnRate = value;
            }
        }
    }

    // coral spawn
    private int m_coralSpawn = 5;

    public int coralSpawn
    {
        get
        {
            return m_coralSpawn;
        }
        set
        {
            if (value < 5)
            {
                Debug.LogError("corals number can't lower than five!");
            }
            else
            {
                m_coralSpawn = value;
            }
        }
    }

    // player script
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCoral (m_coralSpawn);
        playerScript =
            GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        // start spawn red orbs
        StartCoroutine(SpawnRedOrbs());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isGameActive)
        {
            // spawn orbs
            orbCount = GameObject.FindGameObjectsWithTag("Orb").Length;
            if (orbCount == 0)
            {
                int orbsSpawn = Random.Range(0, m_maxOrbSpawnRate + 1);

                // int fishSpawn = Mathf.CeilToInt(Random.Range(0, m_maxOrbSpawnRate));
                SpawnOrbs (orbsSpawn);
            }

            //spawn ships
            shipsCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (shipsCount == 0)
            {
                // int shipSpawn = maxShipSpawnRate;
                SpawnShips (m_maxShipSpawnRate);
            }

            //spawn corals
            coralCount = GameObject.FindGameObjectsWithTag("Coral").Length;
            if (coralCount == 0)
            {
                SpawnCoral (m_coralSpawn);
            }
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

    IEnumerator SpawnRedOrbs()
    {
        while (playerScript.isGameActive)
        {
            redOrbSpawnRate =
                Random.Range(minRedOrbSpawnRate, maxRedOrbSpawnRate);
            yield return new WaitForSeconds(redOrbSpawnRate);
            Instantiate(redOrbPrefab,
            GenerateSpawnPos(0),
            redOrbPrefab.transform.rotation);
        }
    }

    public void SpawnShips(int shipsSpawn)
    {
        // for (int i = 0; i < shipsSpawn; i++)
        // {
        int index = Random.Range(0, m_maxShipIndex + 1);
        Instantiate(shipPrefab[index],
        GenerateSpawnPos(4),
        shipPrefab[index].transform.rotation);
        maxShipSpawnRate++;
        // }
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
