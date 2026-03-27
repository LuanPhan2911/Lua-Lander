using UnityEngine;
using UnityEngine.UI;

public class BuffItemUI : MonoBehaviour
{
    [SerializeField] private BuffManager.BuffType buffType;

    [SerializeField] private Image progressBar;

    [SerializeField] private Color deactivateColor;
    [SerializeField] private Image icon;

    private void Update()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (BuffManager.Instance.IsBuffActive(buffType))
        {
            float remainingTime = BuffManager.Instance.GetBuffTimer(buffType);
            float fillAmount = remainingTime / BuffManager.MAX_BUFF_DURATION;
            progressBar.fillAmount = fillAmount;
            icon.color = Color.white;
        }
        else
        {
            progressBar.fillAmount = 0f;
            icon.color = deactivateColor;
        }
    }

}
