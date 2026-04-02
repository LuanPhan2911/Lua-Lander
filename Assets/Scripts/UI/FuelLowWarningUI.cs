using System;
using UnityEngine;

public class FuelLowWarningUI : MonoBehaviour
{




    private void Start()
    {
        Lander.Instance.OnFuelChanged += Lander_OnFuelChanged;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        Hide();
    }

    private void Lander_OnFuelChanged(object sender, EventArgs e)
    {
        if (BuffManager.Instance.IsBuffActive(BuffManager.BuffType.InfiniteFuel))
        {
            Hide();
            return;
        }
        float nornalizedFuel = Lander.Instance.GetFuelNormalized();

        if (nornalizedFuel <= Lander.LOW_FUEL_THRESHOLD && nornalizedFuel > 0f)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
