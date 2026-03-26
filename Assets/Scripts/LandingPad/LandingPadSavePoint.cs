using UnityEngine;

public class LandingPadSavePoint : LandingPad
{
    [SerializeField] private Transform savePointTransform;


    public Vector3 GetSavePointPosition()
    {
        return savePointTransform.position;
    }
}
