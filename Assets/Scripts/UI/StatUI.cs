using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statMeshPro;

    [SerializeField] private GameObject leftArrowImage;
    [SerializeField] private GameObject rightArrowImage;
    [SerializeField] private GameObject upArrowImage;
    [SerializeField] private GameObject downArrowImage;




    private void Update()
    {
        UpdateStat();
    }

    private void UpdateStat()
    {
        leftArrowImage.SetActive(Lander.Instance.GetSpeedX() < 0f);
        rightArrowImage.SetActive(Lander.Instance.GetSpeedX() >= 0f);
        upArrowImage.SetActive(Lander.Instance.GetSpeedY() >= 0f);
        downArrowImage.SetActive(Lander.Instance.GetSpeedY() < 0f);



        statMeshPro.text =
            $"{GameManager.Instance.GetTimeFormatted()}\n" +
            $"{GameManager.Instance.GetLevelNumber()}\n" +
            $"{Mathf.Round(GameManager.Instance.GetScore())}\n" +
            $"{Mathf.Round(Lander.Instance.GetSpeedX())}\n" +
            $"{Mathf.Round(Lander.Instance.GetSpeedY())}\n";

        ;


    }
}
