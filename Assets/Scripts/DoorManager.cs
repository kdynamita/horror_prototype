using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Put all autoDoor animators")]
    public Animator RadioDoor0Animator;
    public Animator RadioDoor1Animator;
    public Animator RadioDoor2Animator;
    public Animator KitchenDoor0Animator;
    public Animator VeilDoor0Animator;
    public Animator RingDoor0Animator;
    [Header("Put UI stuff if they condition to open/close door")]
    public GameObject walkmanUI;
    public GameObject veilUI;
    public GameObject cupUI;
    public GameObject ringUI;
    [Header("On player's child")]
    public ItemPickup ItemPickup;
    private bool tape1Listened;
    private bool closedRadioDoor0;
    private int counter;
    private bool closeChurchDoor;
    private bool closeChurchDoor2;
    private bool closeChurchDoor3;

    void Start()
    {
        tape1Listened = false;
        closedRadioDoor0 = false;
        counter = 0;
        closeChurchDoor = false;
        closeChurchDoor2 = false;
        closeChurchDoor3 = false;
    }


    void Update()
    {
        // Debug.Log("AutoDoors.RadioDoor0Exit " + AutoDoors.RadioDoor0Exit);
        //  Debug.Log("AutoDoors.OpenRadioDoor12 " + AutoDoors.OpenRadioDoor12);
        // Debug.Log("ItemPickup.tapeListen " + ItemPickup.tapeListen + " AutoDoors.RadioDoor0Exit " + AutoDoors.RadioDoor0Exit);

        //----RadioDoor0-----
        //Openning
        if ((Time.time > 2 && closedRadioDoor0 == false) || (AutoDoors.OpenRadioDoor0 == true && walkmanUI.activeInHierarchy == true && ItemPickup.tapeListen == 0) || (AutoDoors.OpenRadioDoor0 == true && veilUI.activeInHierarchy == true && ItemPickup.tapeListen == 1) || (AutoDoors.OpenRadioDoor0 == true && cupUI.activeInHierarchy == true && ItemPickup.tapeListen == 2) || (AutoDoors.OpenRadioDoor0 == true && ringUI.activeInHierarchy == true && ItemPickup.tapeListen == 3) || (AutoDoors.RadioDoor0Exit == false && closeChurchDoor == false && ItemPickup.tapeListen == 1) || (AutoDoors.RadioDoor0Exit == false && closeChurchDoor2 == false && ItemPickup.tapeListen == 2) || (AutoDoors.RadioDoor0Exit == false && closeChurchDoor3 == false && ItemPickup.tapeListen == 3)) // || (ItemPickup.tapeListen == 1) ||
        {
          //  if (AutoDoors.RadioDoor0Exit == false && ItemPickup.tapeListen == 1 && AutoDoors.OpenRadioDoor0 == false && veilUI.activeInHierarchy == false) Debug.Log("Condition is true");
         //   else
         //   {
                RadioDoor0Animator.SetBool("Open", true);
                RadioDoor0Animator.SetBool("Close", false);
              //  Debug.Log("open");
          //  }
            //  counter = 1;
            //  if (RadioDoor0Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) Debug.Log("Finished open animation of RadioDoor0");
        }

        //  Debug.Log("AutoDoors.RadioDoor0Exit: " + AutoDoors.RadioDoor0Exit + ", ItemPickup.tapeListen: " + ItemPickup.tapeListen + ", AutoDoors.OpenRadioDoor0 " + AutoDoors.OpenRadioDoor0 + ", veilUI.activeInHierarchy: " + veilUI.activeInHierarchy);
        //   Debug.Log("closeChurchDoor2: " + closeChurchDoor2);
       // Debug.Log("closedRadioDoor0 should stay true after we pass and close: " + closedRadioDoor0);
        //Closing
        if ((AutoDoors.RadioDoor0Exit == true && ItemPickup.tapeListen == 0) || (AutoDoors.RadioDoor0Exit1 == true && ItemPickup.tapeListen == 1) || (AutoDoors.RadioDoor0Exit1 == true && ItemPickup.tapeListen == 2) || (AutoDoors.RadioDoor0Exit1 == true && ItemPickup.tapeListen == 3))
        {
            RadioDoor0Animator.SetBool("Open", false);
            RadioDoor0Animator.SetBool("Close", true);
            closedRadioDoor0 = true;
           // Debug.Log("closed");

            //door opens before we get first tape
            if (AutoDoors.RadioDoor0Exit1 == true && ItemPickup.tapeListen == 1 && AutoDoors.OpenRadioDoor0 == false && veilUI.activeInHierarchy == false) closeChurchDoor = true;
            if (AutoDoors.RadioDoor0Exit1 == true && ItemPickup.tapeListen == 2 && AutoDoors.OpenRadioDoor0 == false && cupUI.activeInHierarchy == false) closeChurchDoor2 = true;
            if (AutoDoors.RadioDoor0Exit1 == true && ItemPickup.tapeListen == 3 && AutoDoors.OpenRadioDoor0 == false && ringUI.activeInHierarchy == false) closeChurchDoor3 = true;

            //  tape1Listened = false;
            // if (ItemPickup.tapeListen == 1) tape1Listened = true;
        }
        //-----RadioDoor1-----
        //Openning
        if((AutoDoors.OpenRadioDoor1 == true && veilUI.activeInHierarchy == false && ((ItemPickup.tapeListen == 1 || walkmanUI.activeInHierarchy == false)) || (AutoDoors.OpenRadioDoor12 == true && veilUI.activeInHierarchy == true)))
        {
            RadioDoor1Animator.SetBool("Open", true);
            RadioDoor1Animator.SetBool("Close", false);
           // Debug.Log("RadioDoor1 is open");
        }
        //Closing
        else if((AutoDoors.RadioDoor1Exit2 == true && walkmanUI.activeInHierarchy == true) || (AutoDoors.RadioDoor1Exit == true && ItemPickup.tapeListen == 1 && veilUI.activeInHierarchy == false))
        { 
            RadioDoor1Animator.SetBool("Open", false);
            RadioDoor1Animator.SetBool("Close", true);
          //  Debug.Log("RadioDoor1 is closed");
        }
        //-----RadioDoor2-------
        //Opening
        if (AutoDoors.OpenRadioDoor2 == true && walkmanUI.activeInHierarchy == false)
        {
            RadioDoor2Animator.SetBool("Open", true);
            RadioDoor2Animator.SetBool("Close", false);
        }
        //Closing
        else if (AutoDoors.RadioDoor2Exit == 2 && walkmanUI.activeInHierarchy == true)
        {
            RadioDoor2Animator.SetBool("Open", false);
            RadioDoor2Animator.SetBool("Close", true);
        }
        //   Debug.Log("Should be 2: AutoDoors.RadioDoor2Exit: " + AutoDoors.RadioDoor2Exit);

        //-----VeilDoor0-------
        //Opening
        if ((AutoDoors.OpenVeilDoor0 == true && ItemPickup.tapeListen == 1 && veilUI.activeInHierarchy == false) || (AutoDoors.OpenVeilDoor02 == true && veilUI.activeInHierarchy == true))
        {
            VeilDoor0Animator.SetBool("Open", true);
            VeilDoor0Animator.SetBool("Close", false);
           // Debug.Log("Veil door open");
        }
        //closing
        else if (AutoDoors.VeilDoor0Exit == true && veilUI.activeInHierarchy == true)
        {
            VeilDoor0Animator.SetBool("Open", false);
            VeilDoor0Animator.SetBool("Close", true);
          //  Debug.Log("Veil door closed");
        }

        //----KitchenDoor0--------
        //Opening
        if ((AutoDoors.OpenKitchenDoor0 == true && ItemPickup.tapeListen == 2 && cupUI.activeInHierarchy == false) || (AutoDoors.OpenKitchenDoor02 > 1 && cupUI.activeInHierarchy == true && AutoDoors.KitchenDoor0Exit2 == 1) || (AutoDoors.OpenKitchenDoor0 == true && ItemPickup.tapeListen == 3 && ringUI.activeInHierarchy == false) || (AutoDoors.OpenKitchenDoor02 > 3 && ItemPickup.tapeListen == 3 && ringUI.activeInHierarchy == true && AutoDoors.KitchenDoor0Exit2 == 3)) //&& AutoDoors.KitchenDoor0Exit == 2
        {
            KitchenDoor0Animator.SetBool("Open", true);
            KitchenDoor0Animator.SetBool("Close", false);
          //  Debug.Log("Kitchen door is open");
        }
        //closing
        else if(((AutoDoors.KitchenDoor0Exit == 1) || (AutoDoors.KitchenDoor0Exit == 3 && AutoDoors.KitchenDoor0Exit2 == 3) || (AutoDoors.KitchenDoor0Exit == 4 && AutoDoors.KitchenDoor0Exit2 == 4)) || ((AutoDoors.KitchenDoor0Exit2 > 0 && AutoDoors.KitchenDoor0Exit > 0 && AutoDoors.KitchenDoor0Exit2 != 3) || (AutoDoors.KitchenDoor0Exit2 == 3 && AutoDoors.KitchenDoor0Exit == 3)))
        {
            KitchenDoor0Animator.SetBool("Open", false);
            KitchenDoor0Animator.SetBool("Close", true);
         //   Debug.Log("Kitchen door is closed");
        }
      //  Debug.Log("AutoDoors.KitchenDoor0Exit2: " + AutoDoors.KitchenDoor0Exit2 + " AutoDoors.KitchenDoor0Exit: " + AutoDoors.KitchenDoor0Exit);
        //-----RingDoor0------
        //Opening
        if ((AutoDoors.OpenRingDoor0 == true && ItemPickup.tapeListen == 3 && ringUI.activeInHierarchy == false) || (AutoDoors.OpenRingDoor1 == true && ItemPickup.tapeListen == 3 && ringUI.activeInHierarchy == true)) //2nd cond AutoDoors.OpenRingDoor2 == true && have ring
        {
            RingDoor0Animator.SetBool("Open", true);
            RingDoor0Animator.SetBool("Close", false);
        }
        //Closing
        else if(AutoDoors.RingDoor0Exit > 0 && ringUI.activeInHierarchy == true)
        {
            RingDoor0Animator.SetBool("Open", false);
            RingDoor0Animator.SetBool("Close", true);
        }
    }
}
