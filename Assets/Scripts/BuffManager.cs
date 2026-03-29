using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public static BuffManager Instance { get; private set; }





    public enum BuffType
    {
        SpeedBoost,
        Shield,
        DoubleScore,
        InfiniteFuel
    }
    public const float MAX_BUFF_DURATION = 15f;
    public const int DOUBLE_SCORE_MULTIPLIER = 2;
    public const int DOUBLE_SPEED_MULTIPLIER = 2;
    public const int THRUSTER_EMISSION_MULTIPLIER = 3;


    private float speedBoostTimer;
    private float shieldTimer;
    private float doubleScoreTimer;
    private float infiniteFuelTimer;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (speedBoostTimer > 0)
            speedBoostTimer -= Time.deltaTime;
        if (shieldTimer > 0)
            shieldTimer -= Time.deltaTime;
        if (doubleScoreTimer > 0)
            doubleScoreTimer -= Time.deltaTime;
        if (infiniteFuelTimer > 0)
            infiniteFuelTimer -= Time.deltaTime;
    }

    public void ActivateBuff(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.SpeedBoost:
                speedBoostTimer = MAX_BUFF_DURATION;

                break;
            case BuffType.Shield:
                shieldTimer = MAX_BUFF_DURATION;

                break;
            case BuffType.DoubleScore:
                doubleScoreTimer = MAX_BUFF_DURATION;

                break;
            case BuffType.InfiniteFuel:
                infiniteFuelTimer = MAX_BUFF_DURATION;

                break;
        }
    }
    public void DeactivateBuff(BuffType buffType)
    {
        switch (buffType)
        {

            case BuffType.SpeedBoost:
                speedBoostTimer = 0f;
                break;
            case BuffType.Shield:
                shieldTimer = 0f;
                break;
            case BuffType.DoubleScore:
                doubleScoreTimer = 0f;
                break;
            case BuffType.InfiniteFuel:
                infiniteFuelTimer = 0f;
                break;
            default:
                break;
        }
    }

    public float GetBuffTimer(BuffType buffType)
    {
        return buffType switch
        {
            BuffType.SpeedBoost => speedBoostTimer,
            BuffType.Shield => shieldTimer,
            BuffType.DoubleScore => doubleScoreTimer,
            BuffType.InfiniteFuel => infiniteFuelTimer,
            _ => 0f
        };
    }

    public bool IsBuffActive(BuffType buffType)
    {
        return GetBuffTimer(buffType) > 0f;
    }

    public int GetScoreMultiplier()
    {
        return IsBuffActive(BuffType.DoubleScore) ? DOUBLE_SCORE_MULTIPLIER : 1;
    }

    public int GetSpeedMultiplier()
    {
        return IsBuffActive(BuffType.SpeedBoost) ? DOUBLE_SPEED_MULTIPLIER : 1;
    }

    public int GetEmissionMultiplier()
    {
        return IsBuffActive(BuffType.SpeedBoost) ? THRUSTER_EMISSION_MULTIPLIER : 1;
    }


    public float GetTimerNormalized(BuffType buffType)
    {

        switch (buffType)
        {
            case BuffType.SpeedBoost:
                return speedBoostTimer / MAX_BUFF_DURATION;
            case BuffType.Shield:
                return shieldTimer / MAX_BUFF_DURATION;
            case BuffType.DoubleScore:
                return doubleScoreTimer / MAX_BUFF_DURATION;
            case BuffType.InfiniteFuel:
                return infiniteFuelTimer / MAX_BUFF_DURATION;
            default: return 0f;

        }
    }


}
