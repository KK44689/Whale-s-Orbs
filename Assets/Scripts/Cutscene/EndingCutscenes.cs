using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingCutscenes : MonoBehaviour
{
    public PlayableDirector director;

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
