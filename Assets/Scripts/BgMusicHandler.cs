using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicHandler : MonoBehaviour
{
    // sound
    private AudioSource mainAudioSource;

    public AudioClip bgSound1;

    public AudioClip bgSound2;

    public AudioClip bgSound3;

    public AudioClip bgSound4;

    // check sounds
    private bool isAlreadyPlay2;

    private bool isAlreadyPlay3;

    private bool isAlreadyPlay4;

    // player
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        mainAudioSource = GetComponent<AudioSource>();

        // play sound
        // mainAudioSource.Stop();
        mainAudioSource.loop = true;
        mainAudioSource.clip = bgSound1;
        mainAudioSource.volume = 1f;
        mainAudioSource.Play();

        // check sounds
        isAlreadyPlay2 = false;
        isAlreadyPlay3 = false;
        isAlreadyPlay4 = false;

        // get player script
        playerScript =
            GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.maxExp == 10)
        {
            if (isAlreadyPlay2 == false)
            {
                mainAudioSource.clip = bgSound2;
                mainAudioSource.Play();
                isAlreadyPlay2 = true;
            }
        }
        if (playerScript.maxExp == 15)
        {
            if (isAlreadyPlay3 == false)
            {
                mainAudioSource.clip = bgSound3;
                mainAudioSource.Play();
                isAlreadyPlay3 = true;
            }
        }
        if (playerScript.maxExp == 20)
        {
            if (isAlreadyPlay4 == false)
            {
                mainAudioSource.clip = bgSound4;
                mainAudioSource.Play();
                isAlreadyPlay4 = true;
            }
        }
    }
}
