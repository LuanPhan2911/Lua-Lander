using System;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float delayBeforePlayingSound = 3f;

    private void Start()
    {
        SoundManager.Instance.OnSoundVolumeChanged += SoundManager_OnSoundVolumeChanged;
        audioSource.Pause();
        audioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused()) return;

        delayBeforePlayingSound -= Time.deltaTime;
        if (delayBeforePlayingSound <= 0f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void SoundManager_OnSoundVolumeChanged(object sender, EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }
}
