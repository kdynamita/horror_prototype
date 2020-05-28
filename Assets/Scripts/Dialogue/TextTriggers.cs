using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTriggers : MonoBehaviour
{
    public DialogueController dialogue;
    public GameObject trigger;
    public bool conditionMet;


    void Update()
    {
        if (dialogue != null)
            if (dialogue.isDone)
                TriggerAction();
    }

    void TriggerAction()
    {
        trigger.SetActive(true);
    }
}
