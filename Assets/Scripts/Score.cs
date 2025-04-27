using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    void Start()
    {
        if (GameManager.PatientHealth >= 100)
        {
            gameObject.GetComponent<TMPro.TMP_Text>().text = "You are feeling better keep up the good Work!.";
        }
        if (GameManager.PatientHealth <= 0)
        {
            gameObject.GetComponent<TMPro.TMP_Text>().text = "You were rushed to hospital after your health decreased.";
        }
    }

}
