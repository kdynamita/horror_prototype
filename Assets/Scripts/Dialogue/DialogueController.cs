using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private float startDelay;
    public float delay;
    private float doneDelay = 10f;
    public TextAsset fullText;
    public TextAsset[] tapeText;
    private string currentText = "";

    public bool isDone;

    [SerializeField] private bool isActive;
    [SerializeField] private bool autoStart;
    [SerializeField] private AudioSource typing;

    public int t;

    private GameObject dialogueBox;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.FindGameObjectWithTag("Dialogue");
        if (autoStart)
            StartCoroutine(ShowText());
    }

    void Update()
    {
        if (isActive && Input.GetKey(KeyCode.Space))
        {
            HideText();
        }

    }

    public IEnumerator ShowText() {
       
        if (!isActive) {
            yield return new WaitForSeconds(startDelay);
            isActive = true;
            fullText = tapeText[t];

            for (int i = 0; i < fullText.text.Length + 1; i++)
            {
                currentText = fullText.text.Substring(0, i);
                this.GetComponent<Text>().text = currentText;

                if (typing != null && !typing.isPlaying)
                    typing.Play();

                yield return new WaitForSeconds(delay);
            }

            isDone = true;

            if (typing != null && typing.isPlaying && autoStart)
                typing.Stop();

            yield return new WaitForSeconds(doneDelay);

            if (!autoStart)
                HideText();
        }
    }   

    public void HideText()
    {
        this.GetComponent<Text>().text = null;
        isActive = false;
        dialogueBox.SetActive(false);
    }
}
