using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static float musicTime;

    private AudioSource audioSource;

    public const int MAX_MUSIC_VOLUME = 10;
    private static int musicVolume = 5;

    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.time = musicTime;
        audioSource.volume = GetMusicVolumeNormalized();
    }
    void Update()
    {
        musicTime = audioSource.time;
    }

    public void ChangeMusicVolume(int newMusicVolume)
    {
        musicVolume = newMusicVolume;
        audioSource.volume = GetMusicVolumeNormalized();


    }
    public int GetMusicVolume()
    {
        return musicVolume;
    }

    private float GetMusicVolumeNormalized()
    {
        return (float)musicVolume / MAX_MUSIC_VOLUME;
    }
}
