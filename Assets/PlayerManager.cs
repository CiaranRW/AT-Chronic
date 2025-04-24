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
            if (GameManager.BadScore == 1)
            {
                SetColourByHex("#dbfcc7");
            }
            else if (GameManager.BadScore == 2)
            {
                SetColourByHex("#C2FFA8");
            }
            else if (GameManager.BadScore == 3)
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
