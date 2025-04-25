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
    //[SerializeField] private GameObject character;

    private void Awake()
    {
        mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
        DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
    }

    protected override IEnumerator RunSceneFlow()
    {
        switch (eventPos)
        {
            case 0:
                yield return ShowDialogue("You’ve started walking to work, but you’re running late.");
                NextEvent();
                break;
            case 1:
                yield return ShowDialogue("Do you take your morning medicine, rush out, or go back to bed?");
                interChange();
                dialogueManager.Disable();
                break;
            case 2:
                HandlePathResults();
                break;
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
    IEnumerator ShowDialogue(string text)
    {
        bool finished = false;
        dialogueManager.StartDialogue(text, () => finished = true);
        yield return new WaitUntil(() => finished);
    }

    private void HandlePathResults()
    {
        if (path == "Leave" || path == "Stay")
            GameManager.BadScore += 1;

        if (path != "Stay")
        {
            GameManager.GoodScore += 1;
            FadeOutAndLoad(2);
        }
        else
        {
            FadeOutAndLoad(1);
        }
    }
}