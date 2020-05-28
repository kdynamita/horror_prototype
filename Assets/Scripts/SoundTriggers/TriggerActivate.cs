using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivate : MonoBehaviour
{
    public GameObject trigger;
    public bool autoStart;

    private void OnTriggerEnter(Collider other)
    {
        if (!autoStart)
            trigger.SetActive(true);
    }

    void Update()
    {
        if (autoStart)
            trigger.SetActive(true);
    }
}
