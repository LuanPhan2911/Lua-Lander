using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private SoundSO[] soundSOArray;

    public enum SoundType
    {
        CoinPickup,
        FuelPickup,
        LandingSuccess,
        LandingCrash,
        GetBuff,
        ShieldBreak,
        Shoot
    }

    private AudioClip GetAudioClip(SoundType soundType)
    {
        foreach (SoundSO soundSO in soundSOArray)
        {
            if (soundSO.type == soundType)
            {
                return soundSO.audioClip;
            }
        }
        return null;
    }


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
            PlaySound(SoundType.LandingSuccess, Camera.main.transform.position);
        }
        else
        {
            PlaySound(SoundType.LandingCrash, Camera.main.transform.position);
        }
    }

    private void Lander_OnFuelPickup(object sender, EventArgs e)
    {
        PlaySound(SoundType.FuelPickup, Camera.main.transform.position);
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        PlaySound(SoundType.CoinPickup, Camera.main.transform.position);
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

    public void PlaySound(SoundType soundType, Vector3 position)
    {
        AudioClip audioClip = GetAudioClip(soundType);
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, GetSoundVolumeNormalized());
        }
    }
}
