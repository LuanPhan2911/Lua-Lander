using UnityEngine;

public class LanderShield : MonoBehaviour
{
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float showDisappearEffectInSeconds = 3f;


    private float alpha = 0.5f;
    private const float MIN_ALPHA = 0.25f;
    private const float MAX_ALPHA = 0.5f;

    private float timer = 0f;


    private void Start()
    {
        shieldMaterial.SetFloat("_Alpha", alpha);
        Hide();
    }
    private void Update()
    {
        if (BuffManager.Instance.IsBuffActive(BuffManager.BuffType.Shield))
        {

            if (BuffManager.Instance.GetBuffTimer(BuffManager.BuffType.Shield) <= showDisappearEffectInSeconds)
            {
                UpdateVisual();
            }
        }
        else
        {
            Hide();
        }


    }

    private void UpdateVisual()
    {
        timer += Time.deltaTime;
        alpha = Mathf.Lerp(MAX_ALPHA, MIN_ALPHA, timer / duration);
        shieldMaterial.SetFloat("_Alpha", alpha);

        if (timer > duration)
        {
            timer = 0f;
        }

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageObject damageObject))
        {
            timer = 0f;
            shieldMaterial.SetFloat("_Alpha", MAX_ALPHA);
            BuffManager.Instance.DeactivateBuff(BuffManager.BuffType.Shield);
            Hide();

            // trigger immune

            Lander.Instance.SetImmnuabled(true);

        }
    }



}
