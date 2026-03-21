using UnityEngine;
using UnityEngine.UI;

public class StatUIFuelBar : MonoBehaviour
{

    [SerializeField] private Image fuelBarImage;

    [SerializeField] private Color highFuelBarColor;
    [SerializeField] private Color averageFuelBarColor;
    [SerializeField] private Color lowFuelBarColor;

    private void Update()
    {
        UpdateFuelBar();

    }

    public void UpdateFuelBar()
    {
        fuelBarImage.fillAmount = Lander.Instance.GetFuelNormalized();

        if (fuelBarImage.fillAmount > 0.5f)
        {
            fuelBarImage.color = highFuelBarColor;
        }
        else if (fuelBarImage.fillAmount > 0.25f)
        {
            fuelBarImage.color = averageFuelBarColor;
        }
        else
        {
            fuelBarImage.color = lowFuelBarColor;
        }
    }
}
