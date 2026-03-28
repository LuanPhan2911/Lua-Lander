using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer flashSpriteRenderer;





    private void Update()
    {
        if (spriteRenderer.enabled)
        {
            flashSpriteRenderer.enabled = false;
        }
        else
        {
            flashSpriteRenderer.enabled = true;
        }
    }

    public IEnumerator FlashInterval(float duration, float speed)
    {
        float elapse = 0f;

        while (elapse < duration)
        {

            if (spriteRenderer.enabled)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            yield return new WaitForSeconds(speed);
            elapse += speed;

        }

    }
    public IEnumerator Flash(float speed)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(speed);
        spriteRenderer.enabled = true;



    }



}
