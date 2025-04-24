using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    void Start()
    {
        int test = GameManager.BadScore;
        string test2 = test.ToString();
        gameObject.GetComponent<TMPro.TMP_Text>().text = test2 + " Bad Choices";
    }

}
