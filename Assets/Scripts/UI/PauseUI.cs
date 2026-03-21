using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{


    [SerializeField] private Button resumeButton;

    [SerializeField] private Button backButton;

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
