using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int PatientHealth = 50;
    public GameObject PP;

    [SerializeField] Texture2D[] Cursors;
    [SerializeField] int frameCount;
    private float frameRate = 1f;
    private int currentFrame;
    private float timer;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.SetCursor(Cursors[1], Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    { 
        if (PatientHealth < 50)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += frameRate;
                currentFrame = (currentFrame + 1) % Cursors.Length;
                Cursor.SetCursor(Cursors[currentFrame], Vector2.zero, CursorMode.Auto);
            }
        }
        if (PatientHealth < 30)
        {
            PP.SetActive(true);
            frameRate = 0.5f;
        }
        if (PatientHealth < 20)
        {
            FindFirstObjectByType<DizzyEffect>().ShowDizzyEffect();
            frameRate = 0.2f;
        }
        if (PatientHealth <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
        if (PatientHealth >= 100)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            PP.SetActive(false);
            frameRate = 1f;
        }
        
    }


    public static void MinorGoodChoice()
    {
        PatientHealth = Mathf.Min(PatientHealth + 5, 100);
    }

    public static void MajorGoodChoice()
    {
        PatientHealth = Mathf.Min(PatientHealth + 15, 100);
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
