using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01_NightEvents : SceneControllerBase
{
    private string path;

    private TMP_Text mainTextObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject nextButton;
    //[SerializeField] private GameObject X;

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
        if (GameManager.PatientHealth <= 40)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You're not feeling great tonight.");
                    break;
                case 1:
                    yield return ChoseAndContinue("Do you try and get back to a routine and take your medicine, or skip it just this once and head straight to bed?");
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
        else if (GameManager.PatientHealth > 40)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You feel surprisingly good�almost too good to bother with your routine.");
                    break;
                case 1:
                    yield return ChoseAndContinue("Do you stick to your routine and take your medicine, or skip it just this once and head straight to bed?");
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
    }

    public void ChoseMedicine()
    {
        path = "Medicine";
        charChange();
        //X.SetActive(false);
        StartCoroutine(ChoseAndContinue("You take the pill, hoping tomorrow feels better."));
    }

    public void ChoseSleep()
    {
        path = "Stay";
        charChange();
        //X.SetActive(false);
        StartCoroutine(ChoseAndContinue("You ignore the medicine and drift off, telling yourself it�s just one night."));
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
            GameManager.MajorBadChoice();
        }
        if (path == "Medicine")
        {
            GameManager.MajorGoodChoice();
        }
        FadeOutAndLoad(1);
    }
}