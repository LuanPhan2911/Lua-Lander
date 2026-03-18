using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour
{

    [SerializeField] private TextMeshPro textMeshPro;







    private void Awake()
    {
        textMeshPro.text = $"x{GetComponent<LandingPad>().GetScoreMultiplier()}";
    }
}
