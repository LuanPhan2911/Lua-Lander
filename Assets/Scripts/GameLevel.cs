using UnityEngine;

public class GameLevel : MonoBehaviour
{

    [SerializeField] private int levelNumber;

    [SerializeField] private Transform spawnLanderTransform;

    [SerializeField] private Transform cinemachineCameraFollowTranfrom;

    [SerializeField] private float zoomOutSize;


    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 GetSpawnLanderPosition()
    {
        return spawnLanderTransform.position;
    }
    public Transform GetCinemachineCameraFollowTransform()
    {
        return cinemachineCameraFollowTranfrom;
    }

    public float GetZoomOutSize()
    {
        return zoomOutSize;
    }
}
