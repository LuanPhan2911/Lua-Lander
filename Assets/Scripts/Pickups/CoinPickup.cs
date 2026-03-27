using UnityEngine;

public class CoinPickup : InteractableObject
{



    [SerializeField] private int scoreAmount = 100;
    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    public int GetScoreAmount()
    {
        return scoreAmount;
    }


}
