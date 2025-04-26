using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private RawImage player;
    private GameObject gameobject;

    private void Awake()
    {
        SceneManager.activeSceneChanged += SceneChange;
    }

    private void Update()
    {
        if (player != null)
        {
            if (GameManager.PatientHealth < 40)
            {
                SetColourByHex("#dbfcc7");
            }
            else if (GameManager.PatientHealth < 30)
            {
                SetColourByHex("#C2FFA8");
            }
            else if (GameManager.PatientHealth < 20)
            {
                player.color = Color.green;
            }
        }
    }
    private void SceneChange(Scene currentScene, Scene nextScene)
    {
        gameobject = GameObject.FindGameObjectWithTag("Player");
        if (gameobject != null)
        {
            player = gameobject.GetComponent<RawImage>();
        }
    }

    private void SetColourByHex(string hexColor)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            player.color = color;
        }
        else
        {
            Debug.Log(color);
        }
    }
}
