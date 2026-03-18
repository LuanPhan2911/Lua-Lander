using System;
using UnityEngine;

using UnityEngine.InputSystem;
public class Lander : MonoBehaviour
{
    private Rigidbody2D landerRigidbody2D;


    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;

    private float fuelAmount = 10f;
    private float maxFuelAmount = 10f;



    private void Awake()
    {
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
            return;
        }

        float softLandingVelocityMagnitude = 5f;
        float velocityMagnitude = collision.relativeVelocity.magnitude;
        if (velocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landing too hard");
            return;
        }
        float minDotVector = 0.9f;
        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        if (dotVector < minDotVector)
        {
            Debug.Log("Landing too steep angle");
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
    }
    private void ConsumpFuel()
    {
        float fuelConsumptionRate = 1f;

        fuelAmount -= fuelConsumptionRate * Time.deltaTime;
    }
}
