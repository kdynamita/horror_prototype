using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoors : MonoBehaviour
{
    [Header("Get script attached to player")]
    public ItemPickup itemPickupScript;
    [Header("Put interactive doors in array:")]
    public GameObject[] interactableDoorsArray;
    [Header("Width of word pop up: 200")]
    public float labelWidth;
    [Header("Height of word pop up: 50")]
    public float labelHeight;
    [Header("Leave empty")]
    public bool doorObject;
    private int numberDoorInteractions;
    private int[] doorInteractionsArray;
    [Header("Put animators in array:")]
    public Animator[] animator;
    private int detected;

    void Start()
    {
        doorInteractionsArray = new int[interactableDoorsArray.Length];
    }

    void Update()
    {
        itemPickupScript.CastingRayToDetect(interactableDoorsArray);

        if (doorObject == true)
        {

            for (int i = 0; i < doorInteractionsArray.Length; i++)
            {
                //add 1 to doorInteractionsArray[i] to check amount of times we interacted with each door (will help determine if its open or closed)
                if (itemPickupScript.item.tag == "Door" + i && itemPickupScript.itemDetected <= 0 && itemPickupScript.interactedWithItem == 0) doorInteractionsArray[i] = doorInteractionsArray[i] + 0; //if detected door but didnt interact yet
                else if(itemPickupScript.item.tag == "Door" + i && itemPickupScript.itemDetected > 0 && itemPickupScript.interactedWithItem == 1) doorInteractionsArray[i] = doorInteractionsArray[i] + 1; //if we detected and interacted                                                                       
            }
            detected = itemPickupScript.itemDetected;
            doorAnimations();
        }
    }

    private void OnGUI()
    {
        for (int i = 0; i < doorInteractionsArray.Length; i++)
        {
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = 30;
            myStyle.normal.textColor = Color.red;

            if (detected > 0 && itemPickupScript.item != null && itemPickupScript.item.tag == "Door" + i && doorInteractionsArray[i] % 2 == 0) GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), "Open", myStyle);
            else if (detected > 0 && itemPickupScript.item != null && itemPickupScript.item.tag == "Door" + i && doorInteractionsArray[i] % 2 == 1) GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), "Close", myStyle);
        }
    }

    private void doorAnimations() //Have array of animators! One for each door + sound script +  3D audio source attached
    {
        for (int i = 0; i < doorInteractionsArray.Length; i++)
        {

                if (itemPickupScript.interactedWithItem == 1 && itemPickupScript.item.tag == "Door" + i && doorInteractionsArray[i] % 2 == 1) //was 0
                {
                    animator[i].SetBool("Open", true);
                    animator[i].SetBool("Close", false);
                }
                else if (itemPickupScript.interactedWithItem == 1 && itemPickupScript.item.tag == "Door" + i && doorInteractionsArray[i] % 2 == 0) //was 1
                {
                    animator[i].SetBool("Open", false);
                    animator[i].SetBool("Close", true);
                }
        }
    }
}
