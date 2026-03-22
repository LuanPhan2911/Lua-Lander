using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{


    [SerializeField] private Button resumeButton;

    [SerializeField] private Button backButton;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnPauseGame();
        });
        backButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnPauseGame();
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });


        musicVolumeSlider.maxValue = MusicManager.MAX_MUSIC_VOLUME;
        musicVolumeSlider.value = MusicManager.Instance.GetMusicVolume();

        soundVolumeSlider.maxValue = SoundManager.MAX_SOUND_VOLUME;
        soundVolumeSlider.value = SoundManager.Instance.GetSoundVolume();

        musicVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            MusicManager.Instance.ChangeMusicVolume((int)value);
        });

        soundVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            SoundManager.Instance.ChangeSoundVolume((int)value);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += Gamager_OnGameUnPaused;

        Hide();
    }

    private void Gamager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
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
