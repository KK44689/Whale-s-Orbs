using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCutscenes : Cutscenes
{
    private int mainPageIndex = 0;

    [SerializeField]
    private GameObject moveButton;

    [SerializeField]
    private GameObject dashButton;

    private CheckCutsceneCondition checkConditionScript;

    void Start()
    {
        StartCutscene();
        checkConditionScript =
            GameObject.Find("cutscenes").GetComponent<CheckCutsceneCondition>();
    }

    void StartCutscene()
    {
        Pages[mainPageIndex].SetActive(true);
        Time.timeScale = 0f;
        moveButton.SetActive(false);
        dashButton.SetActive(false);
    }

    public override void EndCutscene()
    {
        checkConditionScript
            .Cutscenes[checkConditionScript.cutsceneIndex]
            .SetActive(false);
        moveButton.SetActive(true);
        dashButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
