using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallpaper : MonoBehaviour
{
    #region Variable

    [SerializeField] List<GameObject> activateObjects = new List<GameObject>();

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sound;
    [SerializeField] private GameObject trigger;

    #endregion
	
	#region MonoBehaviour Methods
	
	private void Awake()
	{
	}
	
	private void Start()
    {
        
    }
	
	private void Update()
    {
        
    }
	
	#endregion
	
	#region Custom Function
	
    public void PlayAnimation()
    {
        GetComponentInChildren<Animation>().Play();

        if (activateObjects.Count > 0)
        {
            foreach(GameObject go in activateObjects)
            {
                go.SetActive(true);
            }
        }

        //StopMusic();
        PlaySound();
        //DeactivateTrigger();
    }

    //public void StopMusic()
    //{
    //    if (music != null && music.isPlaying)
    //    music.Stop();
    //}

    public void PlaySound()
    {
        if (sound != null)
        sound.Play();
    }

    //public void DeactivateTrigger()
    //{
    //    if (trigger != null)
    //    trigger.SetActive(false);
    //}

	#endregion
}
