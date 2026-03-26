using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float delayBeforePlayingSound = 3f;





    private void Update()
    {
        if (GameManager.Instance.IsGamePaused()) return;

        delayBeforePlayingSound -= Time.deltaTime;
        if (delayBeforePlayingSound <= 0f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


}
