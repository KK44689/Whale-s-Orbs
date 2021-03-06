using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCutscenes : Cutscenes
{
    private int mainPageIndex = 0;

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
    }

    public override void EndCutscene()
    {
        checkConditionScript
            .Cutscenes[checkConditionScript.cutsceneIndex]
            .SetActive(false);
        Time.timeScale = 1f;
    }
}
