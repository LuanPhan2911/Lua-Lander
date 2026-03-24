using Cinemachine;
using UnityEngine;

public class ExplosionShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float shakeForce = 1f;


    public void Shake()
    {
        impulseSource.GenerateImpulse(shakeForce);
    }
}
