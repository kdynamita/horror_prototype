using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItems : MonoBehaviour
{
    [Header("Get script attached to player")]
    public ItemPickup itemPickupScript;
    [Header("Put bride in array:")]
    public GameObject[] bride;
    [Header("Width of word pop up: 200")]
    public float labelWidth;
    [Header("Height of word pop up: 50")]
    public float labelHeight;
    private int detected;
    [Header("Put veil, cup, ring in array:")]
    public GameObject[] itemUIArray;
    [Header("Item prefabs to instantiate")]
    public GameObject veilPrefab;
    public GameObject cupPrefab;
    public GameObject ringPrefab;
    [Header("Empty Objects to place items")]
    public Transform veilPrefabPos;
    public Transform cupPrefabPos;
    public Transform ringPrefabPos;
    [Header("Audio Stuff")]
    public AudioSource audiosource;
    public AudioClip itemPlacing;
    public static bool placedVeil;
    public static bool placedCup;
    public static bool placedRing;


    void Start()
    {
        placedVeil = false;
        placedCup = false;
    }

    void Update()
    {
        itemPickupScript.CastingRayToDetect(bride);

        if (itemPickupScript.brideObject == true) detected = itemPickupScript.itemDetected;

        PlaceItem();
    }

    private void OnGUI()
    {
        if(itemPickupScript.brideObject == true)
        {
            for (int i = 0; i < itemUIArray.Length; i++)
            {
                if(itemPickupScript.item.tag == "Bride" && itemPickupScript.item != null && detected > 0 && itemPickupScript.interacted == false && itemUIArray[i].activeInHierarchy == true)
                {
                    GUIStyle myStyle = new GUIStyle();
                    myStyle.fontSize = 30;
                    myStyle.normal.textColor = Color.red;
                    GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), "place item", myStyle);
                }
            }
        }
    }

    private void PlaceItem()
    {
        for (int i = 0; i < itemUIArray.Length; i++)
        {
            //Place Veil
            if(itemPickupScript.interacted == true && itemUIArray[i].tag == "VeilUI"  && itemPickupScript.item != null && itemPickupScript.item.tag == "Bride" && itemUIArray[i].activeInHierarchy == true)
            {
                Instantiate(veilPrefab, veilPrefabPos.position, Quaternion.identity);
                itemUIArray[i].SetActive(false);
                audiosource.clip = itemPlacing;
                audiosource.Play();
                placedVeil = true;
            }
            //Place cup
            if (itemPickupScript.interacted == true && itemUIArray[i].tag == "CupUI" && itemPickupScript.item != null && itemPickupScript.item.tag == "Bride" && itemUIArray[i].activeInHierarchy == true)
            {
                //EntityManager.Instance.GetEnemyManager().RunEvent(3);
                Instantiate(cupPrefab, cupPrefabPos.position, Quaternion.identity);
                itemUIArray[i].SetActive(false);
                audiosource.clip = itemPlacing;
                audiosource.Play();
                placedCup = true;
            }
            //Place ring
            if (itemPickupScript.interacted == true && itemUIArray[i].tag == "RingUI" && itemPickupScript.item != null && itemPickupScript.item.tag == "Bride" && itemUIArray[i].activeInHierarchy == true)
            {
              //  EntityManager.Instance.GetEnemyManager().RunEvent(3);
                Instantiate(ringPrefab, ringPrefabPos.position, Quaternion.identity);
                itemUIArray[i].SetActive(false);
                audiosource.clip = itemPlacing;
                audiosource.Play();
                placedRing = true;
            }
        }
    }
}
