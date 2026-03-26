using System;
using UnityEngine;

public class SoundVolumeControl : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        SoundManager.Instance.OnSoundVolumeChanged += SoundManager_OnSoundVolumeChanged;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        audioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void SoundManager_OnSoundVolumeChanged(object sender, EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
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
}
