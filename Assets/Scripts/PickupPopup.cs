using TMPro;
using UnityEngine;

public class PickupPopup : MonoBehaviour
{

    [SerializeField] private TextMeshPro textMesh;

    private float ttl = 2f;

    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
