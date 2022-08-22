using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour
{
    public GameObject[] Pages;

    private int frameIndex = -1;

    private int pageIndex = 0;

    [SerializeField]
    private int indexWarpScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // all frames must be inactive before play
        if (Input.GetMouseButtonDown(0))
        {
            ActiveNextFrame();
        }
        if (pageIndex >= Pages.Length)
        {
            EndCutscene();
        }
    }

    void ActiveNextFrame()
    {
        frameIndex++;
        if (frameIndex >= Pages[pageIndex]?.transform.childCount)
        {
            ActiveNextPage();
        }
        else
        {
            Pages[pageIndex]
                .transform
                .GetChild(frameIndex)
                .gameObject
                .SetActive(true);
        }
    }

    void ActiveNextPage()
    {
        pageIndex++;
        if (pageIndex < Pages.Length)
        {
            Pages[pageIndex].SetActive(true);
            frameIndex = -1;
            Pages[pageIndex - 1].SetActive(false);
        }
    }

    public virtual void EndCutscene()
    {
        SceneManager.LoadScene (indexWarpScene);
        Time.timeScale = 1f;
    }
}
