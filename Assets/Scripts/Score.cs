using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    void Start()
    {
        int test = GameManager.PatientHealth;
        string test2 = test.ToString();
        gameObject.GetComponent<TMPro.TMP_Text>().text = "You were rushed to hospital after your health decreased.";
    }

}
