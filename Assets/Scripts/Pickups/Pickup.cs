using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject pickupPopupPrefab;

    private bool IsPickedUpBeforeSavePoint = false;
    public void SpawnPickupPopup(string text)
    {
        GameObject popup = Instantiate(pickupPopupPrefab, transform.position, Quaternion.identity);
        PickupPopup popupScript = popup.GetComponent<PickupPopup>();
        if (popupScript != null)
        {
            popupScript.SetText(text);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public bool GetIsPickedUpBeforeSavePoint()
    {
        return IsPickedUpBeforeSavePoint;
    }
    public void SetIsPickedUpBeforeSavePoint(bool value)
    {
        IsPickedUpBeforeSavePoint = value;
    }
}
