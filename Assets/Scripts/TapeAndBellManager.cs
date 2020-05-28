using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapeAndBellManager : MonoBehaviour
{
    [Header("UI objects")]
    public GameObject walkmanUI;
    public GameObject veilUI;
    public GameObject cupUI;
    [Header("Door animators")]
    public Animator RadioDoor0Animator;
    [Header("tapes in scene not active")]
    public GameObject tape1;
    public GameObject tape2;
    public GameObject tape3;
    public GameObject tape4;

    private int counter;
    private int counter2;
    private int counter3;
    private int counter4;

    private EnemyManager enemyManager = null;

    private float timer = 1.5f;


    [SerializeField] private AudioSource bell;
    public float bellDelay;
    public int bellRing;

    [SerializeField] private AudioSource tapeAudio;



    void Start()
    {
        counter = 0;
        counter2 = 0;
        counter3 = 0;
        counter4 = 0;
        enemyManager = EntityManager.Instance.GetEnemyManager();
    }

    void Update()
    {

        //if have walkman and door is closed
        if (walkmanUI.activeInHierarchy == true && AutoDoors.RadioDoor0Exit1 == true && RadioDoor0Animator.GetCurrentAnimatorStateInfo(0).IsName("RadioDoor0_Close") && RadioDoor0Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && counter == 0)
        {
            Debug.Log("Bell cutscene, 1 ring, 1 missing enemy, tape drops");
            StartCoroutine(PlayBell());

            tape1.SetActive(true);
            counter = 1;
        }
        //if placed veil
        if (PlaceItems.placedVeil == true && counter2 == 0)
        {
            Debug.Log("Bell cutscene, 2 rings, 2 missing enemies, tape drops");
            StartCoroutine(PlayBell2());

            //enemy manager
            tape2.SetActive(true);
            counter2 = 1;
        }
        //if placed cup
        if (PlaceItems.placedCup == true && counter3 == 0)
        {
            Debug.Log("Player has to place cup before bell rings many times and enemies activate, have to grab tape from 1 of them");
            StartCoroutine(PlayBellMulti());

            tape3.SetActive(true);
            counter3 = 1;
        }

        if (PlaceItems.placedRing == true && counter4 == 0)
        {
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Play Exit Cutscene now");
                StartCoroutine(PlayBell());
                tape4.SetActive(true);
                counter4 = 1;
            }
        }
    }


    #region - Dumb but functional Bell functions - 
    public IEnumerator PlayBell()
    {
        if (tapeAudio.isPlaying)
        {
            print("Wait up");
            yield return new WaitForSeconds(bellDelay);
        }

        bell.Play();
        enemyManager.RunEvent(EventEnum.DisableObject, 0);

        yield return new WaitForSeconds(bellDelay);

    }

    public IEnumerator PlayBell2()
    {
        if (tapeAudio.isPlaying)
            yield return new WaitForSeconds(bellDelay);

            enemyManager.RunEvent(EventEnum.DisableObject, 1);

        for (int i = 0; i < 2; i++)
        {
            bell.Play();
            yield return new WaitForSeconds(bellDelay);
        }
    }

    public IEnumerator PlayBellMulti()
    {
        if (tapeAudio.isPlaying)
            yield return new WaitForSeconds(bellDelay);

        //enemyManager.RunEvent(EventEnum.DisableOverrides, 2);
        enemyManager.DisableOverrides();
            enemyManager.RunEvent(EventEnum.DisableObject, 3);

        for (int i = 0; i < bellRing; i++)
        {
            bell.Play();
            yield return new WaitForSeconds(bellDelay);
        }
    }
}

#endregion
