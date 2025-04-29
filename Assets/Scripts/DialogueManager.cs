using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    if (nextButton == null)
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
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            onComplete?.Invoke();
            return;
        }

        if (mainText == null || textBox == null || nextButton == null)
        {
            Debug.LogWarning("DialogueManager is not fully initialized.");
            onComplete?.Invoke();
            return;
        }

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
            nextButton.GetComponent<Button>().onClick.RemoveAllListeners();
            nextButton.GetComponent<Button>().onClick.AddListener(OnNextClicked);
        }
        else
        {
            Debug.LogError("Next button is not assigned.");
        }
    }
}