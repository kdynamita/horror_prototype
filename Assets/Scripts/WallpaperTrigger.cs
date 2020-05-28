using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallpaperTrigger : MonoBehaviour
{
    #region Variable

    [SerializeField] private Enemy enemy = null;
    private bool triggered = false;
    private List<WallpaperTrigger> cols = new List<WallpaperTrigger>();
    #endregion

    #region MonoBehaviour Methods

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == EntityManager.Instance.GetPlayer())
        {
            foreach (WallpaperTrigger wt in cols)
            { 
            triggered = true;
            }
        }
    }

    private void Start()
    {
        cols.AddRange(transform.parent.GetComponentsInChildren<WallpaperTrigger>());
    }

    private void Update()
    {
        if(triggered && enemy.CurrentActiveState())
        {
            enemy.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Custom Function

    #endregion
}
