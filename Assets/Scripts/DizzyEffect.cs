using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DizzyEffect : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float scaleSpeed = 1.5f;
    public float scaleAmount = 0.2f;
    private Image dizzyImage;
    private Vector3 originalScale;

    void Start()
    {
        dizzyImage = GetComponent<Image>();
        dizzyImage.enabled = false;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (dizzyImage.enabled)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            float scaleFactor = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
            transform.localScale = originalScale * scaleFactor;
        }
    }

    public void ShowDizzyEffect()
    {
        dizzyImage.enabled = true;
        transform.localScale = originalScale;
    }

}