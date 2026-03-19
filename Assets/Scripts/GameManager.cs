using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int score = 0;

    private float timer = 0f;

    public static GameManager Instance { get; private set; }

    private bool isTimerRunning = false;
    private static int levelNumber = 1;
    [SerializeField] private List<GameLevel> gameLevels;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup; ;
        Lander.Instance.OnLanded += Lander_OnLanded;

        Lander.Instance.OnStateChanged += Lander_OnStateChanged;
        LoadGameLevel();
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerRunning = e.state == Lander.State.Normal;
    }
    private void LoadGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevels)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Vector3 spawnPosition = gameLevel.GetSpawnLanderPosition();
                Lander.Instance.transform.position = spawnPosition;
                break;
            }
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
        }
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e)
    {
        AddScore(500);
    }

    private void AddScore(int scoreAmount)
    {
        score += scoreAmount;
    }



    public int GetScore()
    {
        return score;
    }
    public float GetTime()
    {
        return timer;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadNextLevel()
    {
        levelNumber++;
        SceneManager.LoadScene(0);
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
