using System;
using UnityEngine;
public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }


    [SerializeField] private ExplosionShake explosionShake;
    [SerializeField] private ParticleSystem pickupCoinEffect;


    [SerializeField] private float softLandingVelocityMagnitude = 10f;
    [SerializeField] float minAngleLanding = 0.7f;

    [SerializeField] private float maxFuelAmount = 10f;
    [SerializeField] private float forceLandingUp = 700f;
    [SerializeField] private float rotateSpeed = 100f;

    [SerializeField] private float fuelConsumptionRate = 1f;

    public State state;
    private Rigidbody2D landerRigidbody2D;

    private const float GRAVITY_SCALE = 0.7f;



    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;


    public const float LOW_FUEL_THRESHOLD = 0.3f;
    public const float AVERAGE_FUEL_THRESHOLD = 0.5f;
    public event EventHandler OnFuelChanged;



    public event EventHandler<OnCoinPickupEventArgs> OnCoinPickup;

    public class OnCoinPickupEventArgs : EventArgs
    {
        public int scoreAmount;
    }
    public event EventHandler OnFuelPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public event EventHandler<OnSavePointReachedEventArgs> OnSavePointReached;

    public class OnSavePointReachedEventArgs : EventArgs
    {
        public LandingPadSavePoint landingPadSavePoint;
    }

    public class OnLandedEventArgs : EventArgs
    {
        public int score;
        public LandedState landedState;
        public float landingSpeed;
        public float landingAngle;
    }

    public enum LandedState
    {
        Success,
        Crash,
        TooFast,
        SteepAngle,

    }

    public enum State
    {
        WaitingToStart,
        Normal,
        GameOver
    }

    private float fuelAmount;




    private void Awake()
    {
        Instance = this;
        fuelAmount = maxFuelAmount;
        landerRigidbody2D = GetComponent<Rigidbody2D>();


        ChangeState(State.WaitingToStart);


    }

    private void FixedUpdate()
    {

        HandleMovement();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (!collision.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            ChangeState(State.GameOver);
            Debug.Log("Crash on terrain");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                landedState = LandedState.Crash,
                landingAngle = 0f,
                landingSpeed = 0f,

            });

            // impulse cinemachine effect 

            explosionShake.Shake();
            return;
        }



        if (landingPad is LandingPadFinish)
        {
            float velocityMagnitude = collision.relativeVelocity.magnitude;
            CalculateScore(landingPad as LandingPadFinish, velocityMagnitude);
        }
        else if (landingPad is LandingPadSavePoint)
        {
            // save point for landing pad
            LandingPadSavePoint landingPadSavePoint = landingPad as LandingPadSavePoint;
            if (landingPadSavePoint == GameManager.Instance.GetLandingPadSavePoint())
            {
                return;
            }


            OnSavePointReached?.Invoke(this, new OnSavePointReachedEventArgs
            {
                landingPadSavePoint = (landingPad as LandingPadSavePoint)
            });

            GameManager.Instance.AddMessge("Save Point");


        }




    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out LandingPad landingPad))
        {

            if (landingPad is LandingPadSavePoint)
            {
                float fuelMultiplier = 10f;
                AddFuel(Time.deltaTime * fuelMultiplier);
            }
        }
    }
    private void HandleMovement()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        switch (state)
        {
            default:
            case State.WaitingToStart:
                landerRigidbody2D.gravityScale = 0f;
                if (GameInput.Instance.IsLanderUp() || GameInput.Instance.IsLanderRight() ||
                    GameInput.Instance.IsLanderLeft()

                    )
                {
                    // if any of the keys is pressed, we will consume fuel


                    ChangeState(State.Normal);


                }
                break;
            case State.Normal:

                if (GameInput.Instance.IsLanderUp() || GameInput.Instance.IsLanderRight() ||
                    GameInput.Instance.IsLanderLeft())
                {
                    // if any of the keys is pressed, we will consume fuel


                    landerRigidbody2D.gravityScale = GRAVITY_SCALE;
                    ConsumpFuel();


                }
                if (fuelAmount <= 0f)
                {
                    // if we have no fuel, we can't apply any force


                    return;
                }






                if (GameInput.Instance.IsLanderUp())
                {

                    landerRigidbody2D.AddForce(forceLandingUp * transform.up * Time.deltaTime);

                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }

                if (GameInput.Instance.IsLanderLeft())
                {

                    landerRigidbody2D.AddTorque(rotateSpeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }

                if (GameInput.Instance.IsLanderRight())
                {

                    landerRigidbody2D.AddTorque(-rotateSpeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

    }
    private void CalculateScore(LandingPadFinish landingPad, float velocityMagnitude)
    {

        if (velocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landing too hard");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                landedState = LandedState.TooFast,
                landingAngle = 0f,
                landingSpeed = velocityMagnitude,

            });
            // impulse cinemachine effect 

            explosionShake.Shake();
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        if (dotVector < minAngleLanding)
        {
            Debug.Log("Landing too steep angle");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                landedState = LandedState.SteepAngle,
                landingAngle = dotVector,
                landingSpeed = velocityMagnitude,

            });
            // impulse cinemachine effect 

            explosionShake.Shake();
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


        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore));
        Debug.Log("Landing success");

        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            score = score,
            landedState = LandedState.Success,
            landingAngle = dotVector,
            landingSpeed = velocityMagnitude,

        });
        landerRigidbody2D.gravityScale = 0f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {


            AddFuel(fuelPickup.GetAddedFuel());
            OnFuelPickup?.Invoke(this, EventArgs.Empty);
            fuelPickup.SpawnPickupPopup("Fuel");
            fuelPickup.Hide();
            GameManager.Instance.AddMessge("Fuel Pickup");

            GameManager.Instance.AddPickedUpItemBeforeSavePoint(fuelPickup);

        }
        if (collision.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnCoinPickup?.Invoke(this, new OnCoinPickupEventArgs
            {
                scoreAmount = coinPickup.GetScoreAmount()
            });
            pickupCoinEffect.Play();

            coinPickup.SpawnPickupPopup("+" + coinPickup.GetScoreAmount());
            coinPickup.Hide();
            GameManager.Instance.AddMessge("Coin Pickup");

            GameManager.Instance.AddPickedUpItemBeforeSavePoint(coinPickup);
        }
    }
    private void ConsumpFuel()
    {
        fuelAmount -= fuelConsumptionRate * Time.deltaTime;
        OnFuelChanged?.Invoke(this, EventArgs.Empty);
    }
    private void AddFuel(float fuelAmount)
    {
        this.fuelAmount += fuelAmount;
        if (this.fuelAmount > maxFuelAmount)
        {
            this.fuelAmount = maxFuelAmount;
        }
        OnFuelChanged?.Invoke(this, EventArgs.Empty);
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

    private void ChangeState(State newState)
    {
        state = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });

    }



    public void ResetToInitialState()
    {
        ChangeState(State.WaitingToStart);
        gameObject.SetActive(true);
        transform.rotation = Quaternion.identity;
        fuelAmount = maxFuelAmount;
    }
}
