using UnityEngine;

public class LandingPad : MonoBehaviour
{

    [SerializeField] private int scoreMultiplier;

    [SerializeField] private bool IsNormalLandingPad = false;


    public int GetScoreMultiplier()
    {
        return scoreMultiplier;
    }

    public bool IsNormal()
    {
        return IsNormalLandingPad;
    }

}
