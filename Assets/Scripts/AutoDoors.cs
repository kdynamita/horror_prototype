using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoors : MonoBehaviour
{
    [Header("Leave all empty, checks if exiting trigger")]
    public static bool RadioDoor0Exit;
    public static bool RadioDoor0Exit1;
    public static bool OpenRadioDoor1;
    public static bool RadioDoor1Exit;
    public static bool RadioDoor1Exit2;
    public static bool OpenRadioDoor2;
    public static int RadioDoor2Exit;
    public static bool OpenRadioDoor12;
    public static bool OpenKitchenDoor0;
    public static int KitchenDoor0Exit;
    public static int KitchenDoor0Exit2;
    public static int OpenKitchenDoor02;
    public static bool OpenRadioDoor0;
    public static bool OpenVeilDoor0;
    public static bool VeilDoor0Exit;
    public static bool OpenVeilDoor02;
    public static bool OpenRingDoor0;
    public static int RingDoor0Exit;
    public static bool OpenRingDoor1;

    private bool test;


    void Start()
    {
       // RadioDoor0Exit = false;
    }


    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //Open RadioDoor0 from veil
            if (this.gameObject.name == "OpenRadioDoor0") OpenRadioDoor0 = true;
            else OpenRadioDoor0 = false;

            //Closing RadioDoor0 when exiting church
            if (this.gameObject.name == "RadioDoor0ExitTrigger") RadioDoor0Exit = true;
            else RadioDoor0Exit = false;

            //closing RadioDoor0 when entering church
             if (this.gameObject.name == "RadioDoor0ExitTrigger2") RadioDoor0Exit1 = true;
            else RadioDoor0Exit1 = false;

            if (this.gameObject.name == "Test") test = true;
            if (this.gameObject.name == "Test2") test = true;

            //Opening RadioDoor1 from church
            if (this.gameObject.name == "OpenRadioDoor1") OpenRadioDoor1 = true;
            else OpenRadioDoor1 = false;

            //Opening RadioDoor1 from Veil
            if (this.gameObject.name == "OpenRadioDoor12") OpenRadioDoor12 = true;
            else OpenRadioDoor12 = false;

            //Closing RadioDoor1 from church
            if (this.gameObject.name == "RadioDoor1ExitTrigger") RadioDoor1Exit = true;
            else RadioDoor1Exit = false;

            if (this.gameObject.name == "RadioDoor1ExitTrigger2") RadioDoor1Exit2 = true;
            else RadioDoor1Exit2 = false;

            //Closing RadioDoor2
            if (this.gameObject.name == "RadioDoor2ExitTrigger") RadioDoor2Exit = RadioDoor2Exit + 1;
            // else RadioDoor2Exit = false;

            //Opening KitchenDoor0 from church
            if (this.gameObject.name == "OpenKitchenDoor0") OpenKitchenDoor0 = true;
            else OpenKitchenDoor0 = false;

            //Opening KitchenDoor0 from cup
            if (this.gameObject.name == "OpenKitchenDoor02" || this.gameObject.name == "OpenKitchenDoor023") OpenKitchenDoor02 = OpenKitchenDoor02 + 1;
            //  else OpenKitchenDoor02 = false;

            //Closing KitchenDoor0
            if (this.gameObject.name == "KitchenDoor0ExitTrigger") KitchenDoor0Exit = KitchenDoor0Exit + 1;
            // else KitchenDoor0Exit = false;
            if (this.gameObject.name == "KitchenDoor0ExitTrigger2") KitchenDoor0Exit2 = KitchenDoor0Exit2 + 1;

            //Opening VeilDoor0 from church
            if (this.gameObject.name == "OpenVeilDoor0") OpenVeilDoor0 = true;
            else OpenVeilDoor0 = false;

            //Opening VeilDoor0 after getting veil
            if (this.gameObject.name == "OpenVeilDoor02") OpenVeilDoor02 = true;
            else OpenVeilDoor02 = false;

            //Closing VeilDoor0
            if (this.gameObject.name == "VeilDoor0ExitTrigger") VeilDoor0Exit = true;
            else VeilDoor0Exit = false;

            //Opening RingDoor0 from church
            if (this.gameObject.name == "OpenRingDoor0") OpenRingDoor0 = true;
            else OpenRingDoor0 = false;

            if (this.gameObject.name == "OpenRingDoor1") OpenRingDoor1 = true;
            else OpenRingDoor1 = false;

            //Closing RingDoor0
            if (this.gameObject.name == "RingDoor0ExitTrigger") RingDoor0Exit = RingDoor0Exit + 1;
           // else RingDoor0Exit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Opening RadioDoor2
        if (this.gameObject.name == "OpenRadioDoor2") OpenRadioDoor2 = true;
        else OpenRadioDoor2 = false;

        if (this.gameObject.name == "Test3") test = true;
    }
}
