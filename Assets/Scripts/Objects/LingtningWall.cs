using UnityEngine;
public class LingtningWall : DamageObject
{

    [SerializeField] private LineRenderer lineRenderer;


    [SerializeField] private BoxCollider2D boxColider2D;


    [SerializeField] private float showDuration = 3f;

    private float timer = 0f;

    private void Update()
    {
        if (timer <= showDuration)
        {
            timer += Time.deltaTime;
            return;
        }
        lineRenderer.enabled = !lineRenderer.enabled;
        boxColider2D.enabled = !boxColider2D.enabled;
        timer = 0f;
    }


}
