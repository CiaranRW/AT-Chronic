using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int BadScore = -1;
    public static int GoodScore = 0;
    //private int personalScore = 0;
    public GameObject PP;

    [SerializeField] Texture2D[] Cursors;
    [SerializeField] int frameCount;
    private float frameRate = 1f;
    private int currentFrame;
    private float timer;

    public int Scene01_Stage = 0;
    public int Scene02_Stage = 0;
    public int Scene03_Stage = 0;
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
        if (BadScore >= 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += frameRate;
                currentFrame = (currentFrame + 1) % Cursors.Length;
                Cursor.SetCursor(Cursors[currentFrame], Vector2.zero, CursorMode.Auto);
            }
        }
        if (BadScore == 2)
        {
            PP.SetActive(true);
            frameRate = 0.5f;
            //personalScore = 2;
        }
        if (BadScore == 3)
        {
            FindFirstObjectByType<DizzyEffect>().ShowDizzyEffect();
            frameRate = 0.2f;
            //personalScore = 3;
        }
    }


}
