using UnityEngine;

public class Meteorite : MonoBehaviour
{

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;


    [SerializeField] private float speed = 0.5f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Awake()
    {
        startPosition = transform.TransformPoint(startTransform.localPosition);
        endPosition = transform.TransformPoint(endTransform.localPosition);
    }


    private void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);

        transform.position = Vector3.Lerp(startPosition, endPosition, time);
    }


}
