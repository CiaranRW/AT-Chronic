using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeartCursor : MonoBehaviour
{
    private Canvas canvas;
    private Camera uiCamera;
    private Image heartImage;
    private Image heartOutline;

    private float beatScale = 0.2f;
    private float beatSpeed = 2f;
    private Vector3 originalScale;

    private int maxHealth = 100; // adjust if your system changes

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        Cursor.visible = false;
        originalScale = heartImage.transform.localScale;
    }

    private void Update()
    {
        FollowMouse();
        AnimateBeat();
        UpdateFill();
    }

    void FollowMouse()
    {
        if (canvas == null || uiCamera == null || heartImage == null) return; // safety check

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            uiCamera,
            out localPoint);

        heartOutline.rectTransform.localPosition = localPoint;
    }

    void AnimateBeat()
    {
        float scale = 1f + Mathf.Sin(Time.time * beatSpeed) * 0.1f;

        // Adjust the heartbeat scale based on patient health
        float healthFactor = Mathf.Clamp01((float)GameManager.PatientHealth / maxHealth); // Value between 0 and 1
        heartOutline.transform.localScale = originalScale * (scale * healthFactor); // Scale the heartbeat based on health
    }

    void UpdateFill()
    {
        float healthPercent = (float)GameManager.PatientHealth / maxHealth;
        heartImage.fillAmount = Mathf.Clamp01(healthPercent);

        beatSpeed = Mathf.Lerp(1f, 5f, 1 - healthPercent);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();
        uiCamera = Camera.main;

        HeartCursorGraphic foundHeart = FindFirstObjectByType<HeartCursorGraphic>();
        HeartOutline foundOutline = FindFirstObjectByType<HeartOutline>();
        if (foundHeart != null)
        {
            heartImage = foundHeart.GetComponent<Image>();
            heartOutline = foundOutline.GetComponent<Image>();
            originalScale = heartImage.transform.localScale;
        }
    }
    private void OnDestroy()
    {
        // unsubscribe when this object gets destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
