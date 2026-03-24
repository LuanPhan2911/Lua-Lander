using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Lander;

public class GameManager : MonoBehaviour
{

    private int score = 0;


    private float timer = 0f;

    public static GameManager Instance { get; private set; }

    private bool isTimerRunning = false;


    private static int levelNumber = 1;
    private static int totalScore = 0;

    [SerializeField] private List<GameLevel> gameLevels;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;


    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

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

        GameInput.Instance.OnMenuButtonPressed += GameInput_OnMenuButtonPressed; ;
    }

    private void GameInput_OnMenuButtonPressed(object sender, System.EventArgs e)
    {
        PauseUnpauseGame();
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerRunning = e.state == Lander.State.Normal;
        if (e.state == Lander.State.Normal)
        {
            cinemachineVirtualCamera.Follow = Lander.Instance.transform;
            ZoomCinemachineCamera.Instance.SetNormalZoomSize();
        }
    }

    public void PauseUnpauseGame()
    {
        if (Time.timeScale == 0f)
        {
            UnPauseGame();

        }
        else
        {
            PauseGame();

        }
    }
    public static void ResetStaticData()
    {
        levelNumber = 1;
        totalScore = 0;
    }
    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevels)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                return gameLevel;
            }
        }
        return null;
    }
    private void LoadGameLevel()
    {
        GameLevel gameLevel = GetGameLevel();
        Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Vector3 spawnPosition = gameLevel.GetSpawnLanderPosition();
        Lander.Instance.transform.position = spawnPosition;
        cinemachineVirtualCamera.Follow = gameLevel.GetCinemachineCameraFollowTransform();
        ZoomCinemachineCamera.Instance.SetZoomOutSize(gameLevel.GetZoomOutSize());
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

    private void Lander_OnCoinPickup(object sender, OnCoinPickupEventArgs e)
    {
        AddScore(e.scoreAmount);
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
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public void LoadNextLevel()
    {
        levelNumber++;
        totalScore += score;
        if (GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }


    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);

    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
    public bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}
