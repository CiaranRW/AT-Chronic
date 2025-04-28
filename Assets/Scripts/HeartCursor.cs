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

    private float reverseChance = 1f;
    private int maxHealth = 100;

    private Vector2 previousMousePosition;
    private RectTransform heartRectTransform;

    private float reverseTime = 0.5f;
    private float reverseTimer = 0f;
    private bool isReversing = false;

    private float resetAfterDriftTime = 0.5f;
    private float driftTimer = 0f;
    private bool drifting = true;

    private bool isReady = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (!isReady)
            return;


        FollowMouseWithRandomDirection();
        AnimateBeat();
        UpdateFill();

        if (drifting)
        {
            driftTimer -= Time.deltaTime;
            if (driftTimer <= 0f)
            {
                ResetHeartToMouse();
                drifting = false;
            }
        }
    }

    private void FollowMouseWithRandomDirection()
    {
        if (canvas == null || uiCamera == null || heartImage == null || heartRectTransform == null)
            return;
        Vector2 mousePosition = Input.mousePosition;
        Vector2 anchoredPos;

        // Convert screen position to local canvas position
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, mousePosition, uiCamera, out anchoredPos))
        {
            Vector2 movementDirection = anchoredPos - previousMousePosition;

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
                int randomValue = Random.Range(1, 10001); // Random value between 1 and 10000
                if (reverseTimer <= 0f && randomValue <= (30 - GameManager.PatientHealth) + reverseChance)
                {
                    isReversing = true;
                    reverseTimer = reverseTime;

                    drifting = true;
                    driftTimer = resetAfterDriftTime;
                }
            }

            heartRectTransform.anchoredPosition += movementDirection;
            previousMousePosition = anchoredPos;
        }
    }
    private void ResetHeartToMouse()
    {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            uiCamera,
            out mousePosition);

        heartRectTransform.localPosition = mousePosition;
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

        if (foundHeart != null && foundOutline != null)
        {
            heartImage = foundHeart.GetComponent<Image>();
            heartOutline = foundOutline.GetComponent<Image>();
            heartRectTransform = heartOutline.GetComponent<RectTransform>();
            originalScale = heartImage.transform.localScale;
            ResetHeartToMouse();
            isReady = true;

            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            isReady = false;

            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
