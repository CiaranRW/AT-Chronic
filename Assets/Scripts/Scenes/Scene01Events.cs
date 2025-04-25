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

    private void Awake()
    {
        if (DialogueManager.Instance != null)
        {
            mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
            DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
            DialogueManager.Instance.SetupButtonListener();
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
                    yield return ChoseAndContinue("You feel a bit tired. Do you want a caffeinated drink? Espresso or Matcha?");
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
        else if (GameManager.Instance.Scene01_Stage == 2)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("Womp womp");
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
    }

    public void ChoseMedicine()
    {
        path = "Medicine";
        charChange();
        StartCoroutine(ChoseAndContinue("You take your morning medicine."));
    }

    public void ChoseLeave()
    {
        path = "Leave";
        charChange();
        StartCoroutine(ChoseAndContinue("You left in a hurry."));
    }

    public void ChoseSleep()
    {
        path = "Stay";
        charChange();
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
        yield return new WaitForSeconds(0.5f);
        eventPos = 0;
        if (path == "Leave" || path == "Stay")
        {
            GameManager.BadScore += 1;
        }

        if (path != "Stay")
        {
            GameManager.GoodScore += 1;
            FadeOutAndLoad(2);
        }
        else
        {
            FadeOutAndLoad(1);
            GameManager.Instance.Scene01_Stage++;
        }
    }
}