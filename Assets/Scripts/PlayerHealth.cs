using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float hp;
    [Header("Should be 99")]
    public float maxHp;
    public bool dead;
    [Header("Amount of damage enemies do: 33")]
    public float dmg;
    [Header("After how many sec can enemies do dmg again: try 0 for now")]
    public float delay;
    [SerializeField]
    private bool isHurt = false;
    [SerializeField]
    private float recoverAmount;
    [Space]
    public CameraShake shake;

    //BLOOD SPLATTER STUFF --------------------------------------
    [Header("Animator of blood effect on canvas")]
    public Animator bloodanimator;
    //  private float bloodtimer = 30;
    [Header("Objects in canvas")]
    public GameObject restart;
    public GameObject quit;
    public GameObject youDead;
    public AudioSource audiosource;
    public AudioClip hurtSound;
    public AudioClip deadSound;

    void Start()
    {
    }

    void Update()
    {
        if (Enemy.attackingPlayer == true && isHurt == false)
        {
            Hurt();
            Debug.Log("player hp is: " + hp);
        }

        if (Enemy.attackingPlayer == false && isHurt == false)
        {
            Heal();
        }

    }

   /* void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isHurt == false)
        {
            Hurt();
            Debug.Log("player hp is: " + hp);
        }
    }*/

    void Hurt()
    {
        isHurt = true;
        hp -= dmg;

        if (shake != null) {
            StartCoroutine(shake.Shake(0.4f, 0.4f));
        }

        if (hp > 0)
        {
            //Sound for hurt
            audiosource.clip = hurtSound;
            audiosource.Play();
            dead = false;
        }
        if (hp <= 0 && !dead)
        {
            //sound for death
            audiosource.clip = deadSound;
            audiosource.Play();
            dead = true;
            //add restart and quit buttons
            restart.SetActive(true);
            quit.SetActive(true);
            youDead.SetActive(true);
        }

        StartCoroutine(HurtRecovery());

        //BLOOD SPLATTER ANIMATION CONTROLLER ------

        //if health is less than 99
        if (hp < 99 && hp >= 66) bloodanimator.SetBool("66Health", true);

        //if health is less than 66
        if (hp < 66 && hp >= 33) bloodanimator.SetBool("33Health", true);

        //if health is less than 25 you die
        if (hp < 19) bloodanimator.SetBool("0Health", true);
    }

    public IEnumerator HurtRecovery()
    {
        yield return new WaitForSeconds(delay);
        isHurt = false;
    }

    void Heal()
    {
        if (hp >= maxHp)
        {
            hp = maxHp;
            bloodanimator.SetBool("66Health", false);
            bloodanimator.SetBool("33Health", false);
        }

        hp += recoverAmount;

        if (hp >= 99)
            bloodanimator.SetBool("66Health", false);

        //if health is less than 99
        if (hp < 99 && hp >= 66) {
            bloodanimator.SetBool("66Health", true);
            bloodanimator.SetBool("33Health", false);
        }

        //if health is less than 66
        if (hp < 66 && hp >= 33) bloodanimator.SetBool("33Health", true);

        //if health is less than 25 you die
        if (hp < 19) bloodanimator.SetBool("0Health", true);
    }
}
