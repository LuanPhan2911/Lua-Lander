using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statMeshPro;

    [SerializeField] private GameObject leftArrowImage;
    [SerializeField] private GameObject rightArrowImage;
    [SerializeField] private GameObject upArrowImage;
    [SerializeField] private GameObject downArrowImage;

    [SerializeField] private Image fuelBarImage;


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

        fuelBarImage.fillAmount = Lander.Instance.GetFuelNormalized();

        statMeshPro.text =
            $"{GameManager.Instance.GetLevelNumber()}\n" +
            $"{Mathf.Round(GameManager.Instance.GetScore())}\n" +
            $"{Mathf.Round(GameManager.Instance.GetTime())}\n" +
            $"{Mathf.Round(Lander.Instance.GetSpeedX())}\n" +
            $"{Mathf.Round(Lander.Instance.GetSpeedY())}\n";

        ;


    }
}
