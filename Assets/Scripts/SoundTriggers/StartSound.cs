using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    public AudioSource audioSound;
    [SerializeField] private bool triggered;
    [SerializeField] private bool autoStart;
    [SerializeField] private float audioVol;

    void Start()
    {
        triggered = false;
    }

    void Update()
    {
        audioVol = audioSound.volume;

        if (autoStart && !audioSound.isPlaying && !triggered) {
            PlaySound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !audioSound.isPlaying && !triggered)
        {
            audioSound.Play();
            triggered = true;
        }
    }

    void PlaySound()
    {
        audioSound.Play();
        triggered = true;
    }
}
