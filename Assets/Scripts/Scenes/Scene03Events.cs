using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene03Events : SceneControllerBase
{
    private string path;

    private TMP_Text mainTextObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject nextButton;

    private void Awake()
    {
        mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
        DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
    }

    protected override IEnumerator RunSceneFlow()
    {
        if (GameManager.Instance.Scene03_Stage == 0)
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
        else if (GameManager.Instance.Scene03_Stage == 1)
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
    }


    public void ChoseLow()
    {
        path = "Low";
        charChange();
        StartCoroutine(ChoseAndContinue("You feel more awake and can focus on your work."));
    }

    public void ChoseHigh()
    {
        path = "High";
        charChange();
        StartCoroutine(ChoseAndContinue("You feel more awake; however, your heart races."));
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
        yield return new WaitForSeconds(2);

        if (path == "Low")
        {
            GameManager.MinorGoodChoice();
        }
        else if (path == "High")
        {
            GameManager.MinorBadChoice();
        }

        SceneManager.LoadScene(4);
    }
}