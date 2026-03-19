using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int score = 0;

    private float timer = 0f;

    public static GameManager Instance { get; private set; }

    private bool isTimerRunning = false;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup; ;
        Lander.Instance.OnLanded += Lander_OnLanded;

        Lander.Instance.OnStateChanged += Lander_OnStateChanged;
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerRunning = e.state == Lander.State.Normal;
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
}
