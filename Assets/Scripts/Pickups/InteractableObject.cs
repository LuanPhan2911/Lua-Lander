using UnityEngine;

public class InteractableObject : MonoBehaviour
{


    private bool IsInteractedBeforeSavePoint = false;
    [SerializeField] private string message = "";
    public void SpawnPickupPopup(string text)
    {

        GameObject popupGameObject = Instantiate(
            GameManager.Instance.GetPopupPrefab(), transform.position, Quaternion.identity);
        Popup popup = popupGameObject.GetComponent<Popup>();
        if (popup != null)
        {
            popup.SetText(text);
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

    public bool GetIsInteractedBeforeSavePoint()
    {
        return IsInteractedBeforeSavePoint;
    }
    public void SetIsInteractedBeforeSavePoint(bool value)
    {
        IsInteractedBeforeSavePoint = value;
    }
    public string GetMessage()
    {
        return message;
    }
}
