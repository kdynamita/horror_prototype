using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private static EntityManager _instance = null;
    public static EntityManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject manager = new GameObject("EntityManager");
                _instance = manager.AddComponent<EntityManager>();
            }
            return _instance;
        }
    }

    #region Variables

    private Transform _player = null;
    private Transform _mainCamera = null;
    private EnemyManager _enemyManager = null;

    #endregion

    #region Public Get Method

    public Transform GetPlayer()
    {
        return _player;
    }

    public Transform GetMainCamera()
    {
        return _mainCamera;
    }

    public EnemyManager GetEnemyManager()
    {
        return _enemyManager;
    }

    #endregion

    #region Public Set Method

    public void SetPlayer(Transform transform)
    {
        _player = transform;
    }

    public void SetMainCamera(Transform transform)
    {
        _mainCamera = transform;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        _enemyManager = manager;
    }

    #endregion
}
