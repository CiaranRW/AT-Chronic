using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class Scene02Events : MonoBehaviour
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
        if (GameManager.BadScore == 0 && GameManager.GoodScore == 1)
        {
            //PP.SetActive(false);
            StartCoroutine(NextDialogue("You feel fine as you walk through the park."));
            yield return new WaitForSeconds(1);
        }
        else if (GameManager.BadScore == 1 && GameManager.GoodScore == 1)
        {
            //PP.SetActive(true);
            StartCoroutine(NextDialogue("Walking through the park you start to feel your heart beat."));
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        fadein.SetActive(false);
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        StartCoroutine(NextDialogue("Do you slow down and drink or continue walking to work?"));
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
        if (path == "Walk")
        {
            GameManager.BadScore += 1;
        }
        else if (path == "Rest")
        {
            GameManager.GoodScore += 1;
        }
        SceneManager.LoadScene(3);
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

    public void RestInteract()
    {
        StartCoroutine(NextDialogue("You take a short rest and drink to stay hydrated."));
        interactables.SetActive(false);
        path = "Rest";
    }

    public void WalkInteract()
    {
        StartCoroutine(NextDialogue("You continue walking."));
        interactables.SetActive(false);
        path = "Walk";
    }

    public void Disable()
    {
        textBox.SetActive(false);
        nextButton.SetActive(false);
    }
}
