using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene04Events : SceneControllerBase
{
    private string path;

    private TMP_Text mainTextObject;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject nextButton;

    private void Awake()
    {
        mainTextObject = textBox.GetComponentInChildren<TMP_Text>();
        DialogueManager.Instance.Init(mainTextObject, textBox, nextButton);
        Cursor.visible = false;
    }

    protected override IEnumerator RunSceneFlow()
    {
        if (GameManager.Instance.Scene04_Stage == 0)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You finally get home after a long day.");
                    break;
                case 1:
                    yield return ChoseAndContinue("It’s dinner time. Do you cook a healthy meal or order junk food?");
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


    private IEnumerator ChoseAndContinue(string line)
    {
        bool finished = false;
        dialogueManager.StartDialogue(line, () => finished = true);
        yield return new WaitUntil(() => finished);
        NextEvent();
    }

    public void GoodInteract()
    {
        path = "Good";
        charChange();
        StartCoroutine(ChoseAndContinue("You cook a healthy meal and feel better for it."));
    }

    public void BadInteract()
    {
        path = "Bad";
        charChange();
        StartCoroutine(ChoseAndContinue("You eat a big unhealthy meal. It felt good at the time, but now you’re not feeling so great."));
    }
    private IEnumerator HandlePathResults()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2);

        if (path == "Good")
        {
            GameManager.MinorGoodChoice();
        }
        else if (path == "Bad")
        {
            GameManager.MinorBadChoice();
        }

        SceneManager.LoadScene(5);
    }
}
