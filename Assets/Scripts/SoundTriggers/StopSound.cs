using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSound : MonoBehaviour
{
    public AudioSource audioSound;
    [SerializeField] private float delay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            StopAudio();
        }
    }

    void StopAudio()
    {
        StartCoroutine(StopAudioCo());
    }

    public IEnumerator StopAudioCo()
    {
        yield return new WaitForSeconds(delay);
        audioSound.Stop();
        gameObject.SetActive(false);
    }

}
