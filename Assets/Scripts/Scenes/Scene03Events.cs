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
        Cursor.visible = false;
    }

    protected override IEnumerator RunSceneFlow()
    {
        if (GameManager.Instance.Scene03_Stage == 0)
        {
            switch (eventPos)
            {
                case 0:
                    yield return ChoseAndContinue("You feel a bit tired. Would you like a caffeinated drinkócoffee or water?");
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


    public void ChoseWater()
    {
        path = "Water";
        charChange();
        StartCoroutine(ChoseAndContinue("You feel more awake and can focus on your work."));
    }

    public void ChoseHigh()
    {
        path = "High";
        charChange();
        StartCoroutine(ChoseAndContinue("You feel more awake, but your heart begins to race."));
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

        if (path == "Water")
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