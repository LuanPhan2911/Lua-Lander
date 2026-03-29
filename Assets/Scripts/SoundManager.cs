using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickup;
    [SerializeField] private AudioClip fuelPickup;
    [SerializeField] private AudioClip landingSuccess;
    [SerializeField] private AudioClip landingCrash;

    [SerializeField] private AudioClip getBuffSound;

    [SerializeField] private AudioClip shieldBreakSound;


    public static SoundManager Instance { get; private set; }

    public const int MAX_SOUND_VOLUME = 10;
    private static int soundVolume = 5;

    public event EventHandler OnSoundVolumeChanged;

    private void Awake()
    {
        Instance = this;


    }

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landedState == Lander.LandedState.Success)
        {
            AudioSource.PlayClipAtPoint(landingSuccess, Camera.main.transform.position, GetSoundVolumeNormalized());
        }
        else
        {
            AudioSource.PlayClipAtPoint(landingCrash, Camera.main.transform.position, GetSoundVolumeNormalized());
        }
    }

    private void Lander_OnFuelPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickup, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void ChangeSoundVolume(int volume)
    {
        soundVolume = volume;
        OnSoundVolumeChanged?.Invoke(this, EventArgs.Empty);



    }
    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return (float)soundVolume / MAX_SOUND_VOLUME;
    }

    public void PlayGetBuffSound()
    {
        AudioSource.PlayClipAtPoint(getBuffSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }
    public void PlayShieldBreakSound()
    {
        AudioSource.PlayClipAtPoint(shieldBreakSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }
}
