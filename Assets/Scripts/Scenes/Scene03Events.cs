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
        //fadein.SetActive(true);

        // Start dialogue based on current game score.
        //yield return ChoseAndContinue("You get in and sit down at work.");

/*        yield return new WaitForSeconds(1);
        fadein.SetActive(false);*/

        switch (eventPos)
        {
            case 0:
                yield return ChoseAndContinue("You feel a bit tired. Do you want a caffeinated drink? Espresso or Matcha?");
                break;
            case 1:
                dialogueManager.Disable();
                interChange();
                // CharChange();
                break;
            case 2:
                yield return HandlePathResults();
                break;
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
            GameManager.GoodScore += 1;
        }
        else if (path == "High")
        {
            GameManager.BadScore += 1;
        }

        SceneManager.LoadScene(4);
    }
}