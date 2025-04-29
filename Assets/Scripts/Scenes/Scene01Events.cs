using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01Events : SceneControllerBase
{
    private string path;

    private TMP_Text mainTextObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject X;

    private void Awake()
    {
        if (DialogueManager.Instance != null)
        {
            mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
            DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
            DialogueManager.Instance.SetupButtonListener();
            Cursor.visible = false;
        }
        else
        {
            Debug.LogError("DialogueManager instance is missing!");
        }
    }

    protected override IEnumerator RunSceneFlow()
    {
        if (GameManager.Instance.Scene01_Stage == 0)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You’ve started walking to work, but you’re running late.");
                    break;
                case 1:
                    yield return ChoseAndContinue("Do you take your morning medicine, rush out, or go back to bed?");
                    break;
                case 2:
                    dialogueManager.Disable();
                    interChange();
                    break;
                case 3:
                    yield return HandlePathResults();
                    break;
            }
        }
        else if (GameManager.Instance.Scene01_Stage == 1)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You have another chance of making this right, do you take the medicine, drive to work or stay in bed?");
                    break;
                case 1:
                    dialogueManager.Disable();
                    interChange();
                    break;
                case 2:
                    yield return HandlePathResults();
                    break;

            }

        }
        else if (GameManager.Instance.Scene01_Stage >= 2)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You are feeling terrible and refuse to take the medicine any longer");
                    break;
                case 1:
                    X.SetActive(true);
                    dialogueManager.Disable();
                    interChange();
                    break;
                case 2:
                    yield return HandlePathResults();
                    break;

            }

        }
    }

    public void ChoseMedicine()
    {
        path = "Medicine";
        charChange();
        X.SetActive(false);
        StartCoroutine(ChoseAndContinue("You take your morning medicine."));
    }

    public void ChoseLeave()
    {
        path = "Leave";
        charChange();
        X.SetActive(false);
        StartCoroutine(ChoseAndContinue("You left in a hurry."));
    }

    public void ChoseSleep()
    {
        path = "Stay";
        charChange();
        X.SetActive(false);
        StartCoroutine(ChoseAndContinue("You sleep in."));
    }

    private IEnumerator ChoseAndContinue(string line)
    {
        bool finished = false;
        dialogueManager.StartDialogue(line, () => finished = true);
        yield return new WaitUntil(() => finished);
        NextEvent();
    }

    private IEnumerator HandlePathResults()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        eventPos = 0;
        if (path == "Stay")
        {
            FadeOutAndLoad(1);
            GameManager.Instance.Scene01_Stage++;
            GameManager.MajorBadChoice();
        }
        else if (path == "Medicine")
        {
            GameManager.MajorGoodChoice();
        }
        if (path != "Stay" && GameManager.Instance.Scene01_Stage == 0)
        {
            FadeOutAndLoad(2);
        }
        else if (path != "Stay" && GameManager.Instance.Scene01_Stage > 0)
        {
            SceneManager.LoadScene("CarScene01");
        }
    }
}