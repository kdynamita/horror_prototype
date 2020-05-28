using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Variables

    //Debug
    [SerializeField] bool enableDebug = false;
    [Space]
    [SerializeField] private Material notVisible = null;
    [SerializeField] private Material visible = null;
    [SerializeField] private bool isActive = false;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 25f;
    [SerializeField] private float incrementSpeed = 0.5f;
    [SerializeField] private bool overrideEnemy = false;

    private int aggroCount = 0;
    private float speed = 1f;
    private bool[] activeState = null;
    private Transform target = null;
    private Vector3 startingPos = Vector3.zero;

    private bool increment = false;

    private Animator animator = null;
    private SkinnedMeshRenderer skinnedMesh = null;
    private MeshFilter[] meshFilters = null;
    private RaycastOnVisible[] raycastObjects = new RaycastOnVisible[] { };

    private float distance = 0f;
    [SerializeField] private float attackCoolDownInit = 2f;
    private float attackCoolDown = 0f;

    //hack
    [SerializeField] private bool look = false;


    // KENLEY'S SOUND VARIABLES
    [SerializeField] private AudioSource move;
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource scare;
    [SerializeField] private bool hasBeenSeen;
    private float randomStart;

    public static bool attackingPlayer;

    #endregion


    #region Monobehaviour Methods

    private void Awake()
    {
        animator = GetComponent<Animator>();
        skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();

        InitializeRaycastObjects();
        speed = minSpeed;
    }

    NavMeshAgent nav;
    private void Start()
    {
        target = EntityManager.Instance.GetPlayer();
        nav = GetComponent<NavMeshAgent>();

        //Find SCARE sound
        scare = GameObject.FindGameObjectWithTag("Scare").GetComponent<AudioSource>();
    }

    private void Update()
    {

        distance = Vector3.Distance(target.position, transform.position);

        if (isActive)
        {
            increment = true;
            InvisibleToPlayer();
        }
        else
        {
            if (increment)
            {
                increment = false;
                aggroCount++;
                speed = Mathf.Clamp(speed + incrementSpeed, minSpeed, maxSpeed);
            }

            VisibleToPlayer();
            startingPos = transform.position;
        }
        Attack();
    }

    #endregion

    #region AI Behaviour

    private void VisibleToPlayer()
    {
        nav.enabled = false;
        skinnedMesh.material = visible;
        //stop animation
        animator.enabled = false;
        //not movable
        GetComponent<NavMeshObstacle>().carving = true;
        //nav.isStopped = true;

        // - - - - - Moving sound STOPS when the player sees it && sudden scary sound plays

        if (move.isPlaying)
            move.Stop();

        if (!scare.isPlaying && !hasBeenSeen)
        {
            scare.Play();
        }

        hasBeenSeen = true;
    }

    private void InvisibleToPlayer()
    {
        if (look)
        {
            skinnedMesh.material = notVisible;
            animator.enabled = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * speed);
        }

        if (!overrideEnemy)
        {
            nav.enabled = true;
            GetComponent<NavMeshObstacle>().carving = false;
            skinnedMesh.material = notVisible;
            //start animation
            animator.enabled = true;
            //look towards player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * speed);
            //move towards player
            //nav.isStopped = false;
            //nav.speed = speed;
            //nav.destination = target.position;
            //nav.Move(Vector3.zero);
            if (distance > 4)
            {
                transform.Translate((Vector3.forward) * Time.deltaTime * speed, Space.Self);

                // - - - Concrete drag sound when the enemy is moving
                if (!move.isPlaying)
                    PlayMove();
                if (scare.isPlaying)
                    scare.Stop();

            }//get more aggressive with each increments
        }
    }

    private void Attack()
    {
        if (distance < 4f && !overrideEnemy)
        {
            attackCoolDown -= Time.deltaTime;

            if (attackCoolDown < 0)
            {
                attackingPlayer = true;
                Debug.Log("attack");
                attackCoolDown = attackCoolDownInit;

                attack.Play();
            }
            else attackingPlayer = false;
        }
    }

    #endregion


    #region Game Functions

    //Private functions
    private void InitializeRaycastObjects()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>();
        //add RaycastOnVisible script to low poly collider
        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].gameObject.GetComponent<RaycastOnVisible>() == null)
            {
                meshFilters[i].gameObject.AddComponent<RaycastOnVisible>();
            }

        }

        raycastObjects = GetComponentsInChildren<RaycastOnVisible>();
        for (int i = 0; i < raycastObjects.Length; i++)
        {
            raycastObjects[i].SetupMeshFilterVertice(i, this, raycastObjects);
        }

        activeState = new bool[raycastObjects.Length];
    }

    //Public functions
    public void UpdateActiveStatus(int index, bool status)
    {
        activeState[index] = status;
        CurrentActiveState();
    }

    public bool CurrentActiveState()
    {
        int eachActive = 0;
        foreach (bool status in activeState)
        {
            if (status)
            {
                eachActive++;
            }
        }

        if (eachActive < activeState.Length)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }

        return isActive;
    }

    public int GetEnemyAggroCount()
    {
        return aggroCount;
    }

    public bool IsDebug()
    {
        return enableDebug;
    }

    public void SetOverride(bool _override)
    {
        overrideEnemy = _override;
    }

    #endregion

    #region Sound Functions
    
    public void PlayMove()
    {
        randomStart = Random.Range(0.0f, 10.0f);
        move.time = randomStart;
        move.Play();
    }



    #endregion


}
