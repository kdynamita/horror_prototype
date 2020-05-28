using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip open;
    public AudioClip close;

    Collider collider;
    float timer = 1.5f;
    bool opened;

    void Start()
    {
        collider = GetComponent<Collider>();
        opened = false;
    }


    void Update()
    {
        if(opened == true)
        {
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                collider.enabled = true;
                timer = 1.5f;
            }
        }
    }

    public void Open()
    {
        if (open != null)
        {
            audioSource.clip = open;
            audioSource.Play();
            collider.enabled = false;
            opened = true;
        }
    }

    public void Close()
    {
        if (close != null)
        {
            audioSource.clip = close;
            audioSource.Play();
            collider.enabled = true;
            opened = false;
        }
    }
}
