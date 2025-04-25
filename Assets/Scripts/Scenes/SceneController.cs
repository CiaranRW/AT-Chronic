using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneControllerBase : MonoBehaviour
{
    public GameObject fadein;
    public GameObject fadeout;
    //public GameObject textBox;
    //public GameObject nextButton;
    public GameObject character;
    public GameObject interactables;

    protected DialogueManager dialogueManager;
    protected int eventPos = 0;

    protected virtual void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        character.SetActive(true);
        interactables.SetActive(false);

        StartCoroutine(SceneStartSequence());
    }

    private IEnumerator SceneStartSequence()
    {
        fadein.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadein.SetActive(false);
        yield return RunSceneFlow();
    }

    public void NextEvent()
    {
        eventPos++;
        StartCoroutine(RunSceneFlow());
    }

    protected abstract IEnumerator RunSceneFlow();

    protected void FadeOutAndLoad(int sceneIndex)
    {
        StartCoroutine(FadeOutSequence(sceneIndex));
    }

    private IEnumerator FadeOutSequence(int sceneIndex)
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void charChange()
    {
        character.SetActive(true);
        interactables.SetActive(false);
    }
    public void interChange()
    {
        character.SetActive(false);
        interactables.SetActive(true);
    }
}