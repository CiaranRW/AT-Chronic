using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private GameObject textBox;
    private GameObject nextButton;
    //private GameObject character;
    private TMP_Text mainText;

    private bool waitingForInput = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init(TMP_Text text, GameObject box, GameObject nextBtn)
    {
    if (nextButton == null) // Only assign if it's null (avoids overwriting)
    {
        mainText = text;
        textBox = box;
        nextButton = nextBtn;
    }
    else
    {
        Debug.LogWarning("DialogueManager is already initialized.");
    }
    }

    public void StartDialogue(string text, Action onComplete)
    {
        StartCoroutine(TypeText(text, onComplete));
    }

    IEnumerator TypeText(string text, Action onComplete)
    {
        textBox.SetActive(true);
        //character.SetActive(true);
        mainText.text = "";

        foreach (char c in text)
        {
            mainText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        nextButton.SetActive(true);
        waitingForInput = true;

        yield return new WaitUntil(() => waitingForInput == false);

        nextButton.SetActive(false);
        onComplete?.Invoke();
    }

    public void OnNextClicked()
    {
        waitingForInput = false;
    }

    public void Disable()
    {
        textBox.SetActive(false);
        nextButton.SetActive(false);
    }

    public void SetupButtonListener()
    {
        if (nextButton != null)
        {
            // Clear any existing listeners to avoid duplicates
            nextButton.GetComponent<Button>().onClick.RemoveAllListeners();

            // Reattach the OnNextClicked method to the OnClick listener
            nextButton.GetComponent<Button>().onClick.AddListener(OnNextClicked);
        }
        else
        {
            Debug.LogError("Next button is not assigned.");
        }
    }
}