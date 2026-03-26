using System;
using UnityEngine;

public class LanderThrusterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;


    void Start()
    {
        Lander.Instance.OnUpForce += Lander_OnUpForce;
        Lander.Instance.OnLeftForce += Lander_OnLeftForce;
        Lander.Instance.OnRightForce += Lander_OnRightForce;
        Lander.Instance.OnBeforeForce += Lander_OnBeforeForce;
    }



    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    private void Lander_OnRightForce(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Lander_OnLeftForce(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Lander_OnUpForce(object sender, EventArgs e)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
