using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterPaticleSystem;
    [SerializeField] private ParticleSystem middleThrusterPaticleSystem;
    [SerializeField] private ParticleSystem rightThrusterPaticleSystem;

    [SerializeField] private GameObject crashParticleSystem;

    [SerializeField] private int rateEmissionOverTime = 150;


    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private float flashSpeed;


    private Coroutine flashCoroutine;





    private void Awake()
    {


        Lander.Instance.OnUpForce += Lander_OnUpForce;
        Lander.Instance.OnBeforeForce += Lander_OnBeforeForce;

        Lander.Instance.OnLeftForce += Lander_OnLeftForce;
        Lander.Instance.OnRightForce += Lander_OnRightForce;

        Lander.Instance.OnLanded += Lander_OnLanded;

    }

    private void Update()
    {
        if (Lander.Instance.IsImmnueled())
        {
            if (flashCoroutine != null)
            {
                return;
            }
            flashCoroutine = StartCoroutine(flashEffect.FlashInterval(
                Lander.Instance.GetImmnueableDuration(), flashSpeed));
        }
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landedState != Lander.LandedState.Success)
        {
            Instantiate(crashParticleSystem, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnableParticleSystem(leftThrusterPaticleSystem, true);
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnableParticleSystem(rightThrusterPaticleSystem, true);
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnableParticleSystem(leftThrusterPaticleSystem, false);
        SetEnableParticleSystem(middleThrusterPaticleSystem, false);
        SetEnableParticleSystem(rightThrusterPaticleSystem, false);
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnableParticleSystem(leftThrusterPaticleSystem, true);
        SetEnableParticleSystem(middleThrusterPaticleSystem, true);
        SetEnableParticleSystem(rightThrusterPaticleSystem, true);
    }

    private void SetEnableParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;

        emissionModule.enabled = enabled;

        emissionModule.rateOverTime = rateEmissionOverTime * BuffManager.Instance.GetEmissionMultiplier();
    }
}
