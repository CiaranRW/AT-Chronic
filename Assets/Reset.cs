using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.PatientHealth = 70;
        GameManager.Instance.Scene01_Stage = 0;
        GameManager.Instance.Scene02_Stage = 0;
        GameManager.Instance.Scene03_Stage = 0;
        GameManager.Instance.Scene04_Stage = 0;

        AudioManager.Instance?.PlayMenuMusic();
    }
}
