using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene05Events : SceneControllerBase
{
    private string path;

    private TMP_Text mainTextObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject X;

    private void Awake()
    {
        mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
        DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
        Cursor.visible = false;
    }

    protected override IEnumerator RunSceneFlow()
    {
        if (GameManager.Instance.Scene05_Stage == 0)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You drive to work instead");
                    break;
                case 1:
                    yield return ChoseAndContinue("Do you smoke a cigarette or not?");
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
        if (GameManager.Instance.Scene05_Stage == 1)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You drive to work again");
                    break;
                case 1:
                    yield return ChoseAndContinue("You can't resist the urge to smoke again");
                    break;
                case 2:
                    X.SetActive(true);
                    dialogueManager.Disable();
                    interChange();
                    break;
                case 3:
                    yield return HandlePathResults();
                    break;
            }

        }
    }


    private IEnumerator ChoseAndContinue(string line)
    {
        bool finished = false;
        dialogueManager.StartDialogue(line, () => finished = true);
        yield return new WaitUntil(() => finished);
        NextEvent();
    }

    public void SmokeInteract()
    {
        path = "Smoke";
        X.SetActive(false);
        charChange();
        StartCoroutine(ChoseAndContinue("You smoke."));
    }

    public void ContinueInteract()
    {
        path = "Continue";
        charChange();
        StartCoroutine(ChoseAndContinue("You continue driving to work resisting the urge to smoke."));
    }
    private IEnumerator HandlePathResults()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2);


        if (path == "Smoke")
        {
            GameManager.MajorBadChoice();
            GameManager.Instance.Scene05_Stage += 1;
        }

        SceneManager.LoadScene(3);
    }
}
