using System;
using UnityEngine;
using UnityEngine.UI;

public class StatUIFuelBar : MonoBehaviour
{

    [SerializeField] private Image fuelBarImage;

    [SerializeField] private Color highFuelBarColor;
    [SerializeField] private Color averageFuelBarColor;
    [SerializeField] private Color lowFuelBarColor;


    [SerializeField] protected Gradient infiniteGradientColor;
    private const float timmerMax = 0.2f;
    private float timer;

    private void Start()
    {
        Lander.Instance.OnFuelChanged += Lander_OnFuelChanged;
        UpdateStatFuelBar();
    }
    private void Update()
    {

        if (BuffManager.Instance.IsBuffActive(BuffManager.BuffType.InfiniteFuel))
        {
            if (timer <= timmerMax)
            {
                timer += Time.deltaTime;
                return;
            }
            fuelBarImage.color = infiniteGradientColor.Evaluate(UnityEngine.Random.value);
            timer = 0;
        }
    }
    private void Lander_OnFuelChanged(object sender, EventArgs e)
    {
        UpdateStatFuelBar();

    }

    private void UpdateStatFuelBar()
    {
        if (BuffManager.Instance.IsBuffActive(BuffManager.BuffType.InfiniteFuel))
        {
            return;
        }

        fuelBarImage.fillAmount = Lander.Instance.GetFuelNormalized();

        if (fuelBarImage.fillAmount > Lander.AVERAGE_FUEL_THRESHOLD)
        {
            fuelBarImage.color = highFuelBarColor;
        }
        else if (fuelBarImage.fillAmount > Lander.LOW_FUEL_THRESHOLD)
        {
            fuelBarImage.color = averageFuelBarColor;
        }
        else
        {
            fuelBarImage.color = lowFuelBarColor;
        }
    }




}
