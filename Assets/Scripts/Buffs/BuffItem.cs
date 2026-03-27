using UnityEngine;

public class BuffItem : InteractableObject
{
    [SerializeField] private BuffManager.BuffType type;


    public BuffManager.BuffType GetBuffType()
    {
        return type;
    }
}
