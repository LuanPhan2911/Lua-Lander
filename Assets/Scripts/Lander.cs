using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }
    private Rigidbody2D landerRigidbody2D;



    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;

    public event EventHandler OnCoinPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;

    public class OnLandedEventArgs : EventArgs
    {
        public int score;
        public LandedState landedState;
        public int multiplier;
        public float landingSpeed;
        public float landingAngle;
    }

    public enum LandedState
    {
        Success,
        Crash,
        TooFast,
        SteepAngle
    }

    private float fuelAmount;
    private float maxFuelAmount = 10f;



    private void Awake()
    {
        Instance = this;
        fuelAmount = maxFuelAmount;
        landerRigidbody2D = GetComponent<Rigidbody2D>();


    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        if (fuelAmount <= 0f)
        {
            // if we have no fuel, we can't apply any force
            return;
        }

        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            // if any of the keys is pressed, we will consume fuel

            ConsumpFuel();
        }



        if (Keyboard.current.upArrowKey.isPressed)
        {
            float force = 700f;
            landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);

            OnUpForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            float rotateSpeed = 100f;
            landerRigidbody2D.AddTorque(rotateSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            float rotateSpeed = -100f;
            landerRigidbody2D.AddTorque(rotateSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            Debug.Log("Crash on terrain");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                landedState = LandedState.Crash,
                landingAngle = 0f,
                landingSpeed = 0f,
                multiplier = 0
            });
            return;
        }

        float softLandingVelocityMagnitude = 5f;
        float velocityMagnitude = collision.relativeVelocity.magnitude;
        if (velocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landing too hard");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                landedState = LandedState.TooFast,
                landingAngle = 0f,
                landingSpeed = velocityMagnitude,
                multiplier = landingPad.GetScoreMultiplier()
            });
            return;
        }
        float minDotVector = 0.9f;
        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        if (dotVector < minDotVector)
        {
            Debug.Log("Landing too steep angle");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                landedState = LandedState.SteepAngle,
                landingAngle = dotVector,
                landingSpeed = velocityMagnitude,
                multiplier = landingPad.GetScoreMultiplier()
            });
            return;
        }

        float maxScoreAmountLandingAngle = 100f;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle -
            Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;


        float maxScoreAmoutSpeed = 100f;
        float landingSpeedScore = maxScoreAmoutSpeed * (softLandingVelocityMagnitude - velocityMagnitude);


        Debug.Log("LandingScore " + landingAngleScore);
        Debug.Log("SpeedScore " + landingSpeedScore);


        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());
        Debug.Log("Landing success");

        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            score = score,
            landedState = LandedState.Success,
            landingAngle = dotVector,
            landingSpeed = velocityMagnitude,
            multiplier = landingPad.GetScoreMultiplier()
        });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {


            fuelAmount += fuelPickup.GetAddedFuel();
            if (fuelAmount > maxFuelAmount)
            {
                fuelAmount = maxFuelAmount;

            }

            fuelPickup.DestroySelf();

        }
        if (collision.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }
    }
    private void ConsumpFuel()
    {
        float fuelConsumptionRate = 1f;

        fuelAmount -= fuelConsumptionRate * Time.deltaTime;
    }

    public float GetSpeedX()
    {
        return landerRigidbody2D.velocity.x;
    }
    public float GetSpeedY()
    {
        return landerRigidbody2D.velocity.y;
    }
    public float GetFuel()
    {
        return fuelAmount;
    }
    public float GetFuelNormalized()
    {
        return fuelAmount / maxFuelAmount;
    }
}
