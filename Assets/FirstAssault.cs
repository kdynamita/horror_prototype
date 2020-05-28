using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAssault : MonoBehaviour
{
    #region Variable

    [SerializeField] private float[] initTimers = new float[] { };
    [SerializeField] private GameObject enemy = null;

    private List<float> timers = new List<float>();

    private Transform cam = null;
    private Transform player = null;
    private Player playerScript = null;

    private bool lookForwardWalk = false;
    private bool attacked = false;
    private bool reaction = false;

    private Quaternion currentRot = Quaternion.identity;
    private Quaternion currentCamRot = Quaternion.identity;
    private Vector3 currentPos = Vector3.zero;

    private Quaternion newRot = Quaternion.Euler(0, 90, 0);
    private Vector3 newPos = Vector3.zero;




    [SerializeField] private float[] ratio = new float[] { };

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        newPos = transform.GetChild(0).position;
    }

    private void Start()
    {
        player = EntityManager.Instance.GetPlayer();
        cam = EntityManager.Instance.GetMainCamera();
        playerScript = player.GetComponent<Player>();
        timers.AddRange(initTimers);
        ratio = new float[timers.Count];
    }

    private void Update()
    {
        if (!reaction)
        {
            if (!attacked)
            {
                if (lookForwardWalk)
                {
                    TimerRatio(0);
                    TimerRatio(1);
                    player.position = Vector3.Lerp(currentPos, newPos, ratio[0]);
                    player.rotation = Quaternion.Slerp(currentRot, newRot, ratio[1]);
                    cam.localRotation = Quaternion.Slerp(currentCamRot, Quaternion.identity, ratio[1]);

                    if (ratio[0] >= 1)
                    {
                        enemy.transform.position = transform.GetChild(1).position;
                        enemy.transform.rotation = transform.GetChild(1).rotation;
                        attacked = true;
                    }
                }
            }
            else
            {
                TimerRatio(2);
                player.rotation = Quaternion.Slerp(currentRot, Quaternion.Euler(0, -90, 0), ratio[2]);
                if (ratio[2] >= 1)
                {
                    currentPos = player.position;
                    reaction = true;
                }
            }
        }
        else
        {
            TimerRatio(3);
            player.position = Vector3.Lerp(currentPos, currentPos + (player.forward * -6f), ratio[3]);
            if (ratio[3] >= 1)
            {
                enemy.GetComponent<Enemy>().SetOverride(false);
                playerScript.OverrideControl(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            if (!lookForwardWalk)
            {
                playerScript.OverrideControl(true);
                currentRot = player.rotation;
                currentCamRot = cam.localRotation;
                currentPos = player.position;
                lookForwardWalk = true;
            }
        }
    }

    #endregion

    #region Custom Function

    private void TimerRatio(int index)
    {
        timers[index] -= Time.deltaTime;
        ratio[index] = 1 - (timers[index] / initTimers[index]);
    }

    #endregion
}
