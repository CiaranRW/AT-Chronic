using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01Events : MonoBehaviour
{
    public DialogueManager dialogue;
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
        yield return new WaitForSeconds(1);
        fadein.SetActive(false);
        StartCoroutine(NextDialogue("You have taking up walking to work to help with your heart condition but you are running late."));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        StartCoroutine(NextDialogue("Do you take your time and take your morning medicine, quickly make up for lost time and continue with your day or sleep in?"));
        yield return new WaitForSeconds(0.5f);
    }

    /*    IEnumerator EventFour()
        {
            nextButton.SetActive(false);
            string text = "Let's move!";
            StartCoroutine(NextDialogue(text));
            yield return new WaitForSeconds(0.5f);
        }*/
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
        if (path == "Leave" || path == "Stay")
        { 
            GameManager.BadScore += 1;
        }
        if (path != "Stay")
        {
            SceneManager.LoadScene(2);
            GameManager.GoodScore += 1;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        audioManager.fadeIn = true;
    }


    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
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

    public void MedicineInteract()
    {
        StartCoroutine(NextDialogue("You take your time and take your morning medicine, missing out could be dangerous."));
        interactables.SetActive(false);
        path = "Medicine";
    }
    public void LeaveInteract()
    {
        StartCoroutine(NextDialogue("You left in a hurry."));
        interactables.SetActive(false);
        path = "Leave";
    }
    public void BedInteract()
    {
        StartCoroutine(NextDialogue("You sleep in."));
        interactables.SetActive(false);
        path = "Stay";
    }

    public void Disable()
    {
        textBox.SetActive(false);
        nextButton.SetActive(false);
    }

}
