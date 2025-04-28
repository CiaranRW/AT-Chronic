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
    public float fadeDuration = 0.5f;
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

    private bool isChangingAudio = false;

    private void Update()
    {
        if (GameManager.PatientHealth != personalScore && !isChangingAudio)
        {
            StartCoroutine(ChangeAudioRoutine());
        }
    }

    private IEnumerator ChangeAudioRoutine()
    {
        isChangingAudio = true;

        AudioClip newClip = BGM.clip;
        AudioSource HeartbeatSound = HeartBeat.GetComponent<AudioSource>();

        if (GameManager.PatientHealth >= 60)
        {
            newClip = sixtyBPM;
            HeartBeat.SetActive(false);
        }
        else if (GameManager.PatientHealth <= 50 && GameManager.PatientHealth > 30)
        {
            HeartBeat.SetActive(true);
            HeartbeatSound.volume = 0.2f;
        }
        else if (GameManager.PatientHealth <= 30 && GameManager.PatientHealth > 20)
        {
            HeartBeat.SetActive(true);
            HeartbeatSound.volume = 0.5f;
            HeartbeatSound.pitch = 1.2f;
            newClip = eightyBPM;
        }
        else if (GameManager.PatientHealth <= 20 && GameManager.PatientHealth > 10)
        {
            HeartBeat.SetActive(true);
            HeartbeatSound.volume = 0.8f;
            HeartbeatSound.pitch = 1.5f;
            newClip = eightyBPM;
        }

        if (NeedsMusicChange(newClip))
        {
            yield return FadeAudio(BGM.volume, 0f, fadeDuration);

            BGM.clip = newClip;
            BGM.Play();

            yield return FadeAudio(0f, 0.7f, fadeDuration);
        }
        else if (!BGM.isPlaying)
        {
            BGM.Play();
        }


        personalScore = GameManager.PatientHealth;
        isChangingAudio = false;
    }

    private bool NeedsMusicChange(AudioClip newClip)
    {
        return BGM.clip != newClip;
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
