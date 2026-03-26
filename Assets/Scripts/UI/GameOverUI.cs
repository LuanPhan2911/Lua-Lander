using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI timeText;


    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });


    }
    private void Start()
    {
        totalScoreText.text = "Total Score: " + GameManager.Instance.GetTotalScore();
        timeText.text = "Time: " + GameManager.Instance.GetTime();
    }
}
