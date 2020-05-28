using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeactivate : MonoBehaviour
{
    public GameObject trigger;
    public bool autoStart;

    private void OnTriggerEnter(Collider other)
    {
        if (!autoStart)
            trigger.SetActive(false);
    }

    void Update()
    {
        if (autoStart)
            trigger.SetActive(false);
    }
}
