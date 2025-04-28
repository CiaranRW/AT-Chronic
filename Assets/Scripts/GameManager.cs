using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int PatientHealth = 60;
    public GameObject PP;

    public int Scene01_Stage = 0;
    public int Scene02_Stage = 0;
    public int Scene03_Stage = 0;
    public int Scene04_Stage = 0;
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Cursor.visible = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //Cursor.SetCursor(Cursors[1], Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    { 
        if (PatientHealth < 50)
        {
            PP.SetActive(true);
        }
        if (PatientHealth < 30)
        {
            FindFirstObjectByType<DizzyEffect>().ShowDizzyEffect();
            PP.SetActive(true);
        }
        if (PatientHealth <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
        if (PatientHealth >= 100)
        {
            SceneManager.LoadScene("EndScene");
        }
        else if (PatientHealth > 50)
        {
            PP.SetActive(false);
        }
        
    }


    public static void MinorGoodChoice()
    {
        PatientHealth = Mathf.Min(PatientHealth + 5, 100);
    }
    public static void MajorGoodChoice()
    {
        PatientHealth = Mathf.Min(PatientHealth + 10, 100);
    }
    public static void MinorBadChoice()
    {
        PatientHealth = Mathf.Max(PatientHealth - 5, 0);
    }
    public static void MajorBadChoice()
    {
        PatientHealth = Mathf.Max(PatientHealth - 20, 0);
    }


}
