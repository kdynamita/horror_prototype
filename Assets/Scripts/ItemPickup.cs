using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour
{
    [Header("How far player is from object to pick: ")]
    public float maxDistance;
    RaycastHit hit;
    [Header("Leave empty")]
    public GameObject item;
    [Header("Leave empty")]
    public int itemDetected;
    [Header("Put pickable objects in array")]
    public GameObject[] itemArray;
    [Header("Put UI images of pickable objects array")]
    public GameObject[] itemUIArray;
    [Header("Leave empty")]
    public int interactedWithItem;
    [Header("Width of word pop up: 200")]
    public float labelWidth;
    [Header("Height of word pop up: 50")]
    public float labelHeight;
    string itemUI;
    [Header("Leave empty")]
    public bool interacted;
   // bool inventoryObject;
    [Header("Get script attached to player")]
    public InteractableDoors interactableDoors;
    [Header("Audio stuff, source should be on player")]
    public AudioSource audiosource;
    public AudioSource audiosource1;
    public AudioClip itemPickup;
    public AudioClip itemPlacing;
    public AudioClip objectiveSuccess;
    public AudioClip walkmantape;
    [Header("Array of audioclips for Tapes")]
    public AudioClip[] TapeSound;
    [Header("Leave empty")]
    public bool brideObject;
    public int tapeListen;
    [SerializeField] private int tapeCounter;
     private float timer;
    private bool startTape;
    private int counter;
    private string itemName;

    #region - SUBTITLES VARIABLES -
    public AudioSource success;

    public GameObject dialogueBox;
    public DialogueController subtitles;
    #endregion


    void Start()
    {
        tapeListen = 0;
        tapeCounter = 0;
        timer = 3f;
        counter = 0;
    }

    void Update()
    {
        CastingRayToDetect(itemArray);

        //  if (interactableDoors.doorObject == false && interacted == true) Debug.Log("Destroy item"); // Destroy(item);
        //PlaceItem function call

      //  Debug.Log("itemName " + itemName);
       // Debug.Log("tapeCounter " + tapeCounter);
      //  Debug.Log("tapeListen " + tapeListen);

     /*   if(itemName == "Tape" + 1)
        {
            audiosource.clip = TapeSound[0];
            audiosource.Play();
            tapeListen = tapeListen + 1;
        }*/
        //Play tapes!


        if (tapeCounter == 1 || tapeCounter == 2 || tapeCounter == 3 || tapeCounter == 4 || tapeCounter == 5)
        {
            timer = timer - Time.deltaTime;

            if (timer <= 0 && counter == 0)
            {
                tapeListen = tapeListen + 1;
              //  Debug.Log("tapeListen " + tapeListen);

                counter = 1;
                timer = 3f;

                for (int i = 0; i < TapeSound.Length; i++) 
                {
                  //  Debug.Log("i " + i + ", itemName " + itemName);

                    if ((TapeSound[i] != null && itemName == "Tape" + i))
                    {
                        audiosource1.clip = TapeSound[i-1];
                        audiosource1.Play();
                        TapeSound[i-1] = null;
                      //  Debug.Log("Went into normal loop");
                    }
                }
                if(itemName == "Tape4")
                {
                    audiosource1.clip = TapeSound[3];
                    audiosource1.Play();
                   // Debug.Log("Went into hard code");
                }
            }
        }
        AddInventoryAndSound();

        if (tapeCounter >= 4)
            StartCoroutine(GoToFinalScene() );
    }

    public void CastingRayToDetect(GameObject [] inputArray)
    {
        itemDetected = 0;
        interactedWithItem = 0;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance)) //Try SphereCast?????????????????
        {
            // Debug.Log("hit.transform.root.tag " + hit.transform.root.tag);
            // Debug.DrawRay(transform.position, transform.forward, Color.green);
         //   if(hit.transform.tag != "Untagged") Debug.Log("hit.transform.tag: " + hit.transform.tag);

            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] != null && hit.transform.tag != null && (hit.transform.tag == inputArray[i].tag))
                {
                    itemDetected = itemDetected + 1;
                    item = inputArray[i];

                 //   Debug.Log("CastRay itemDetected " + itemDetected);

                    //check if item is a door
                    if (item.tag == "Door" + i && interactableDoors != null) interactableDoors.doorObject = true;
                    else if (interactableDoors != null) interactableDoors.doorObject = false;

                    //check if item is bride
                    if (item.tag == "Bride") brideObject = true;
                    else brideObject = false;

                    //Check if we interacted with the item
                    if (itemDetected > 0 && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)))
                    {
                        interacted = true;
                        interactedWithItem = interactedWithItem + 1;
                        if (inputArray[i] != null && (interactableDoors == null || interactableDoors.doorObject == false) && brideObject == false) itemUI = inputArray[i].tag + "UI";
                    }
                    else interacted = false;

                 //  if (interacted == true) interactedWithItem = interactedWithItem + 1;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (itemDetected > 0 && interacted == false && (interactableDoors == null || interactableDoors.doorObject == false) && brideObject == false)
        {
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = 30;
            myStyle.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), "pick up", myStyle);
        }
    }

    private void AddInventoryAndSound()
    {
        if (itemDetected > 0 && interacted == true && (interactableDoors == null || interactableDoors.doorObject == false) && brideObject == false)
        {
            for (int i = 0; i < itemUIArray.Length; i++)
            {
                if (itemUI == itemUIArray[i].tag)
                {
                    //activate UI pop up for the item
                    itemUIArray[i].SetActive(true);
                    itemName = item.tag;
                    Destroy(item);

                    //--- PLAY "COLLECT ITEM" SOUND ----
                    audiosource.clip = itemPickup;
                    audiosource.Play();

                    //Play SUCCESS sound
                    success.Play();

                    if (itemUI == "RecorderUI" && itemUIArray[i].tag == "RecorderUI")
                    {

                        success.Play();

                        //Play tape1 sound
                        audiosource1.clip = walkmantape;
                        audiosource1.Play();

                     //   print("Subtitles Activated");
                        dialogueBox.SetActive(true);
                        subtitles.t = 0;
                        subtitles.StartCoroutine(subtitles.ShowText());
                    }
                   // Debug.Log("i in the itemsUIArray: " + i);
                   if (itemUIArray[i].tag == "Tape" + i + "UI")
                   {
                        tapeCounter = tapeCounter + 1;
                     //   Debug.Log("tapeCounter " + tapeCounter);
                        counter = 0;

                        //Play SUCCESS sound
                        success.Play();

                        //  print("Subtitles Activated");
                        dialogueBox.SetActive(true);
                        subtitles.t = i;
                        subtitles.StartCoroutine(subtitles.ShowText());

                        if(tapeCounter == 4)
                        {
                            StartCoroutine(GoToFinalScene());
                        }
                    }
                }
            }
        }
    }


    public IEnumerator GoToFinalScene()
    {
        yield return new WaitForSeconds(25f);
        SceneManager.LoadScene("EndScene");
    }
}
