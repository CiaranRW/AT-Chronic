using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource BGM;
    private int personalScore = 0;
    public AudioClip sixtyBPM;
    public AudioClip eightyBPM;
    public float fadeDuration = 2f;
    public bool fadeIn;
    public bool fadeOut;
    private GameObject HeartBeat;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        BGM = gameObject.GetComponentInChildren<AudioSource>();
        HeartBeat = GameObject.FindGameObjectWithTag("HeartBeat");
        HeartBeat.SetActive(false);
    }

    private void Update()
    {
        if (fadeIn == true)
        {
            FadeIn();
            fadeIn = false;
            BGM.Play();
        }
        if (fadeOut == true)
        {
            FadeOut();
            fadeOut = false;
        }
        if (GameManager.BadScore == personalScore)
        {
            AudioSource HeartbeatSound = HeartBeat.GetComponent<AudioSource>();
            if (GameManager.BadScore == 0)
            {
                BGM.clip = sixtyBPM;
            }
            else if (GameManager.BadScore == 1)
            {
                HeartBeat.SetActive(true);
                HeartbeatSound.volume = 0.2f;
            }
            else if (GameManager.BadScore == 2)
            {
                HeartbeatSound.volume = 0.5f;
                HeartbeatSound.pitch = 1.2f;
                BGM.clip = eightyBPM;
            }
            else if (GameManager.BadScore == 3)
            {
                HeartbeatSound.volume = 0.8f;
                HeartbeatSound.pitch = 1.5f;
                BGM.clip = eightyBPM;
            }
            BGM.Play();
            personalScore++;
        }
    }
    public void FadeIn()
    {
        StartCoroutine(FadeAudio(0f, 0.7f, fadeDuration));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeAudio(0.7f, 0f, fadeDuration));
    }

    IEnumerator FadeAudio(float startVolume, float endVolume, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            BGM.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        BGM.volume = endVolume;

    }

}
