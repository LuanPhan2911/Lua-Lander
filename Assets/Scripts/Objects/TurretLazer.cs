using UnityEngine;
public class LingtningWall : DamageObject
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem chargedShotParticleSystem;


    [SerializeField] private BoxCollider2D boxColider2D;


    [SerializeField] private float shotDuration = 3f;
    [SerializeField] private float chargedDuration = 3f;

    [SerializeField] private float minEmissionRate = 0f;
    [SerializeField] private float maxEmissionRate = 100f;

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

        lineRenderer.enabled = true;
        boxColider2D.enabled = true;
    }



}
