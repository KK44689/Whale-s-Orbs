using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCutsceneCondition : MonoBehaviour
{
    public GameObject[] Cutscenes;

    private PlayerController playerScript;

    public int cutsceneIndex { get; private set; }

    private bool cutscence1_Played = false;

    private bool cutscence2_Played = false;

    private bool cutscence3_Played = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript =
            GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.maxExp == 10 && !cutscence1_Played)
        {
            cutsceneIndex = 0;
            Cutscenes[cutsceneIndex].SetActive(true);
            Time.timeScale = 0f;
            cutscence1_Played = true;
        }
        if (playerScript.maxExp == 15 && !cutscence2_Played)
        {
            cutsceneIndex = 1;
            Cutscenes[cutsceneIndex].SetActive(true);
            Time.timeScale = 0f;
            cutscence2_Played = true;
        }
        if (playerScript.maxExp == 20 && !cutscence3_Played)
        {
            cutsceneIndex = 2;
            Cutscenes[cutsceneIndex].SetActive(true);
            Time.timeScale = 0f;
            cutscence3_Played = true;
        }
    }
}
