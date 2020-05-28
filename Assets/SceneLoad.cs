using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public float delay;
    public int sceneLoad;

    // Update is called once per frame
    void Update()
    {
        if (sceneLoad == 1)
            PlayGame();

        if (sceneLoad == 2)
            FinishGame();

        if (sceneLoad == 3)
            Ending();

        else
        {
            return;
        }
    }

    void PlayGame()
    {
        StartCoroutine(PlayGameCo());
    }

    public IEnumerator PlayGameCo()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level2");
    }

    void FinishGame()
    {
        StartCoroutine(FinishGameCo());
    }

    public IEnumerator FinishGameCo()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("CreditsScene");
    }

    void Ending()
    {
        StartCoroutine(EndingCo());
    }

    public IEnumerator EndingCo()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("EndScene");
    }
}
