using UnityEngine;

public class FuelPickup : InteractableObject
{

    [SerializeField] private float addedFuel = 5f;



    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public float GetAddedFuel()
    {
        return addedFuel;
    }
}
