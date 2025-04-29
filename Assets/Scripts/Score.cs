using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text ending;
    public Image background;
    public Sprite goodEnding;
    public Sprite badEnding;

    void Start()
    {
        if (GameManager.PatientHealth >= 100)
        {
            score.text = "You are feeling better keep up the good Work!";
            ending.text = "Good Ending";
            background.sprite = goodEnding;
            
        }
        if (GameManager.PatientHealth <= 0)
        {
            score.text = "You were rushed to hospital after your health decreased.";
            ending.text = "Bad Ending";
            background.sprite = badEnding;
        }
    }

}
