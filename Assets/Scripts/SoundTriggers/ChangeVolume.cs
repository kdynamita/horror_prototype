using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] private AudioSource targetAudio;
    [SerializeField] private float incrementVol;
    [SerializeField] private float targetVolume;
    [SerializeField] private bool triggered;
    [SerializeField] private bool decrease;
    
    void Start()
    {
        triggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            triggered = true;
    }

    void Update()
    {
        if (triggered && targetAudio.isPlaying)
            ChangeVol();
    }

    void ChangeVol()
    {

        // - - - DECREASING VOLUME
        if (triggered && targetAudio.volume > targetVolume && decrease)
            targetAudio.volume -= incrementVol;

        else if (decrease && targetAudio.volume < targetVolume)
        {
            targetAudio.volume = targetVolume;
            return;
        }


        // - - - - INCREASING VOLUME
        if (triggered && targetAudio.volume < targetVolume && !decrease)
            targetAudio.volume += incrementVol;

        else if (!decrease && targetAudio.volume > targetVolume)
        {
            targetAudio.volume = targetVolume;
            return;
        }

    }
}
