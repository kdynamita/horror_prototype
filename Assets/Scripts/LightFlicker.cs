using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    #region Flicker Interval Variables

    [Header("Interval Variables")]
    [Range(0f,60f)]
    public float minTime = 0.05f;
    [Range(0f, 60f)]
    public float maxTime = 1.2f;

    #endregion

    private float timer;
    private Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            light.enabled = !light.enabled;
            //AudioBehaviour.Play();
            timer = Random.Range(minTime, maxTime);
        }
    }

    #region Script Update Log
    // Last Update: Ronaldo (4/17/2019 | 12:01)
    #endregion
}
