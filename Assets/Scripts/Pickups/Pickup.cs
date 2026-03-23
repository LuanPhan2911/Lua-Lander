using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject pickupPopupPrefab;
    public void SpawnPickupPopup(string text)
    {
        GameObject popup = Instantiate(pickupPopupPrefab, transform.position, Quaternion.identity);
        PickupPopup popupScript = popup.GetComponent<PickupPopup>();
        if (popupScript != null)
        {
            popupScript.SetText(text);
        }
    }
}
