using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene02Events : SceneControllerBase
{
    private string path;

    private TMP_Text mainTextObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject nextButton;

    private void Awake()
    {
        mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
        DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
        //eventPos = 0;
    }

    protected override IEnumerator RunSceneFlow()
    {
        if (GameManager.Instance.Scene01_Stage == 0)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("Do you slow down and drink or continue walking to work?");
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

    public void ChoseRest()
    {
        path = "Rest";
        charChange();
        StartCoroutine(ChoseAndContinue("You take a short rest and drink to stay hydrated."));
    }

    public void ChoseWalk()
    {
        path = "Walk";
        charChange();
        StartCoroutine(ChoseAndContinue("You continue walking."));
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

    private IEnumerator HandlePathResults()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2);

        if (path == "Walk")
        {
            GameManager.MajorBadChoice();
        }
        else if (path == "Rest")
        {
            GameManager.MinorGoodChoice();
        }

        SceneManager.LoadScene(3);
    }
}