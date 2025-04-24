using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene04Events : MonoBehaviour
{
    public GameObject fadein;
    public GameObject fadeout;
    public GameObject textBox;

    private int eventPos = 0;
    private string path;
    private AudioManager audioManager;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject interactables;
    private GameObject character;

    private void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        character = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        fadein.SetActive(true);
        StartCoroutine(NextDialogue("You get home after a long day."));
        yield return new WaitForSeconds(1);
        fadein.SetActive(false);
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        StartCoroutine(NextDialogue("It is time for dinner, do you cook a healthy meal or order junk food?"));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator EventTwo()
    {
        Disable();
        CharChange();
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator EventEnd()
    {
        audioManager.fadeOut = true;
        nextButton.SetActive(false);
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2);
        if (path == "Bad")
        {
            GameManager.BadScore += 1;
        }
        else if (path == "Good")
        {
            GameManager.GoodScore += 1;
        }
        SceneManager.LoadScene(5);
        audioManager.fadeIn = true;
    }

    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine (EventOne());
        }
        else if (eventPos == 2)
        {
            StartCoroutine(EventTwo());
        }
        else if (eventPos == 3)
        {
            StartCoroutine(EventEnd());
        }
    }

    IEnumerator NextDialogue(string text)
    {
        textBox.SetActive(true);
        character.SetActive(true);
        textToSpeak = text;
        mainTextObject.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => textLength == currentTextLength);
        nextButton.SetActive(true);
        eventPos++;
    }

    public void CharChange()
    {
        interactables.SetActive(true);
        character.SetActive(false);
    }

    public void GoodInteract()
    {
        StartCoroutine(NextDialogue("You managed to cook yourself a healthy meal and feel better."));
        interactables.SetActive(false);
        path = "Good";
    }

    public void BadInteract()
    {
        StartCoroutine(NextDialogue("You had a big unhealthy meal which felt nice in the moment but now not so great."));
        interactables.SetActive(false);
        path = "Bad";
    }

    public void Disable()
    {
        textBox.SetActive(false);
        nextButton.SetActive(false);
    }
}
