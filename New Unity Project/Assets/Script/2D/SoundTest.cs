using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{

    public AudioClip[] audioClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SoundTest_1();
    }

    void SoundTest_1()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StopAndPlay(audioClips[0]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StopAndPlay(audioClips[1]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            StopAndPlay(audioClips[2]);
        }
    }

    void StopAndPlay(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = clip;
        audioSource.Play();
    }
}