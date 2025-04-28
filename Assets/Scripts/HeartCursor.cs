using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeartCursor : MonoBehaviour
{
    private Canvas canvas;
    private Camera uiCamera;
    private Image heartImage;
    private Image heartOutline;

    private float beatSpeed = 2f;
    private Vector3 originalScale;

    private float reverseChance = 2f;
    private int maxHealth = 100;

    private Vector2 previousMousePosition;
    private RectTransform heartRectTransform;

    private float reverseTime = 2f;
    private float reverseTimer = 0f;
    private bool isReversing = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        originalScale = heartImage.transform.localScale;
        heartRectTransform = heartOutline.GetComponent<RectTransform>();
    }

    private void Update()
    {
        FollowMouseWithRandomDirection();
        AnimateBeat();
        UpdateFill();
        
    }

    private void FollowMouseWithRandomDirection()
    {
        if (canvas == null || uiCamera == null || heartImage == null) return;

        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 movementDirection = currentMousePosition - previousMousePosition;

        if (isReversing)
        {
            movementDirection = -movementDirection;
            reverseTimer -= Time.deltaTime;

            if (reverseTimer <= 0f)
            {
                isReversing = false;
                reverseTimer = 0f;
            }
        }
        else
        {
            int randomValue = Random.Range(1, 10001);
            if (reverseTimer <= 0f && randomValue < reverseChance)
            {
                isReversing = true;
                reverseTimer = reverseTime;
            }
        }

        heartRectTransform.localPosition += (Vector3)movementDirection;

        previousMousePosition = currentMousePosition;
    }

    void AnimateBeat()
    {
        float scale = 0.5f + Mathf.Sin(Time.time * beatSpeed) * 0.1f;
        heartOutline.transform.localScale = originalScale * scale;
    }

    void UpdateFill()
    {
        float healthPercent = (float)GameManager.PatientHealth / maxHealth;
        heartImage.fillAmount = Mathf.Clamp01(healthPercent);

        beatSpeed = Mathf.Lerp(1f, 20f, 1 - healthPercent);
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
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
