using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    #region - Variables -
    private Player player;


    [SerializeField] private AudioSource walking;
    [SerializeField] private AudioSource running;
    #endregion

    void Start()
    {
        player = GetComponent<Player>();
        walking = GameObject.Find("Walking").GetComponent<AudioSource>();
        running = GameObject.Find("Running").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FootSteps();
    }

    void FootSteps()
    {
        if (walking != null || running != null)
        {

            if (player.isMoving && 
                !player.isRunning && 
                !walking.isPlaying)
            {
                running.Stop();
                walking.Play();
            }

            else if (player.isMoving && 
                 player.isRunning &&
                 !running.isPlaying
                )
            {
                walking.Stop();
                running.Play();
            }

            else if (!player.isMoving &&
                 !player.isRunning)
            {
                walking.Stop();
                running.Stop();
            }
        }
    }
}
