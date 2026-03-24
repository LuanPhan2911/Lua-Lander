using System;
using UnityEngine;

public class FuelLowWarningUI : MonoBehaviour
{

    [SerializeField] private AudioSource warningAudioSource;


    private void Start()
    {
        Lander.Instance.OnFuelChanged += Lander_OnFuelChanged;
        Lander.Instance.OnLanded += Lander_OnLanded;

        SoundManager.Instance.OnSoundVolumeChanged += SoundManager_OnSoundVolumeChanged;

        warningAudioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        Hide();
    }

    private void Lander_OnFuelChanged(object sender, EventArgs e)
    {
        float nornalizedFuel = Lander.Instance.GetFuelNormalized();

        if (nornalizedFuel <= Lander.LOW_FUEL_THRESHOLD && nornalizedFuel > 0f)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void SoundManager_OnSoundVolumeChanged(object sender, EventArgs e)
    {
        warningAudioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }



    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
