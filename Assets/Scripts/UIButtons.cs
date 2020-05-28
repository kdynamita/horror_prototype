using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }


    void Update()
    {
        if (player.hp <= 0 && Input.GetKey(KeyCode.Space))
            Restart();

        if (player.hp <= 0 && Input.GetKey(KeyCode.Escape))
            Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
