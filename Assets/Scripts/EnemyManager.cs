using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventEnum { None, EnableObject, DisableObject, Scale2x, DisableOverrides };

public class EnemyManager : MonoBehaviour
{
    #region Variable

    [SerializeField] private EnemyManagerVariable[] enemies = new EnemyManagerVariable[] { };
    [SerializeField] private EnemyManagerData[] events = new EnemyManagerData[] { };
    private float delay = 0.1f;

    //test


    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        EntityManager.Instance.SetEnemyManager(this);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        }
        else if (delay > -50f)
        {
            EnableOverrides();
            Debug.Log("override true");
            delay = -100f;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enemy.GetComponent<Enemy>().SetOverride(enemies[i].overrideScript);
        }
    }

    #endregion

    #region Custom Function

    [ContextMenu("Setup")]
    private void SetupVariable()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Rename(i);
            enemies[i].enemy = transform.GetChild(i).gameObject;
        }
    }

    public void SetOverride(int index, bool b)
    {
        enemies[index].overrideScript = b;
    }

    public GameObject GetEnemy(int index)
    {
        return enemies[index].enemy;
    }

    [ContextMenu("enable")]
    public void EnableOverrides()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].overrideScript = true;
        }
    }

    [ContextMenu("disable")]
    public void DisableOverrides()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].overrideScript = false;
        }
    }

    #endregion

    #region Events

    [ContextMenu("RunEvent")]
    public void Debuuug()
    {
        RunEvent(EventEnum.DisableObject, 0);
    }

    public void RunEvent(EventEnum type, int eventIndex)
    {
        switch (type)
        {
            case EventEnum.None:
                break;
            case EventEnum.EnableObject:
                EnableObject(eventIndex);
                break;
            case EventEnum.DisableObject:
                DisableObject(eventIndex);
                break;
            case EventEnum.Scale2x:
                Scale2x(eventIndex);
                break;
            case EventEnum.DisableOverrides:
                DisableOverride(eventIndex);
                break;
        }
    }

    private void EnableObject(int index)
    {

        foreach (GameObject go in events[index].entities)
        {
            go.SetActive(true);
        }
    }

    private void DisableObject(int index)
    {

        foreach (GameObject go in events[index].entities)
        {
            go.SetActive(false);
        }
    }

    private void DisableOverride(int index)
    {
        foreach (GameObject go in events[index].entities)
        {
            go.GetComponent<Enemy>().SetOverride(false);
        }
    }

    private void Scale2x(int index)
    {
        foreach (GameObject go in events[index].entities)
        {
            go.transform.localScale = Vector3.one * 4;
        }
    }



    #endregion

    [ContextMenu("Raaawr")]
    private void EnableAll()
    {
        DisableOverrides();
            }
}


[System.Serializable]
public class EnemyManagerVariable
{
    [HideInInspector]
    public string name;
    public GameObject enemy;
    public bool overrideScript = false;

    public void Rename(int index)
    {
        name = "Enemy " + index;
    }
}

[System.Serializable]
public class EnemyManagerData
{
    [HideInInspector]
    public string name;
    public EventEnum events;
    public GameObject[] entities;
}