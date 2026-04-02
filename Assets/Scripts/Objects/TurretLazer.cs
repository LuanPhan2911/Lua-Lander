using UnityEngine;
public class TurretLazer : DamageObject
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem chargedShotParticleSystem;


    [SerializeField] private BoxCollider2D boxColider2D;


    [SerializeField] private float shotDuration;
    [SerializeField] private float chargedDuration;

    [SerializeField] private float minEmissionRate;
    [SerializeField] private float maxEmissionRate;

    [SerializeField] private AudioSource turretChargedSound;

    private bool isCharged = false;

    private float timer = 0f;



    private void Update()
    {
        timer += Time.deltaTime;
        if (!isCharged)
        {

            if (timer <= chargedDuration)
            {
                // charging to shot
                ChargeShot();
            }
            else
            {
                isCharged = !isCharged;
                timer = 0f;
                // shot

                SoundManager.Instance.PlaySound(SoundManager.SoundType.Shoot, transform.position);
            }
        }
        else
        {
            if (timer <= shotDuration)
            {
                Shot();
            }
            else
            {
                isCharged = !isCharged;
                timer = 0f;
            }

        }

    }

    private void ChargeShot()
    {
        if (!chargedShotParticleSystem.isPlaying)
        {
            chargedShotParticleSystem.Play();
        }
        if (!turretChargedSound.isPlaying)
        {
            turretChargedSound.Play();
        }
        ParticleSystem.EmissionModule emission = chargedShotParticleSystem.emission;
        emission.rateOverTime = Mathf.Lerp(minEmissionRate, maxEmissionRate, timer / chargedDuration);

        lineRenderer.enabled = false;
        boxColider2D.enabled = false;
    }
    private void Shot()
    {
        if (chargedShotParticleSystem.isPlaying)
        {
            chargedShotParticleSystem.Stop();
        }
        if (turretChargedSound.isPlaying)
        {
            turretChargedSound.Stop();
        }

        lineRenderer.enabled = true;
        boxColider2D.enabled = true;
    }



}
