using UnityEngine;

public class LandingVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterPaticleSystem;
    [SerializeField] private ParticleSystem middleThrusterPaticleSystem;
    [SerializeField] private ParticleSystem rightThrusterPaticleSystem;


    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();

        lander.OnUpForce += Lander_OnUpForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;

        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;

    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnableParticleSystem(rightThrusterPaticleSystem, true);
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnableParticleSystem(leftThrusterPaticleSystem, true);
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
    }
}
