using UnityEngine;

public class GameLevel : MonoBehaviour
{

    [SerializeField] private int levelNumber;

    [SerializeField] private Transform spawnLanderTransform;




    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 GetSpawnLanderPosition()
    {
        return spawnLanderTransform.position;
    }
}
