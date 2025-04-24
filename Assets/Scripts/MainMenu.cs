using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject fadeIn;
    [SerializeField] GameObject fadeOut;

    private void Start()
    {
        //fadeIn.SetActive(true);
        Screen.fullScreen = true;
    }
    public void StartGame()
    {
        fadeOut.SetActive(true);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3f);
        GameManager.BadScore = 0;
        SceneManager.LoadScene(1);
    }
}


