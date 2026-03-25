using UnityEngine;

public class FallingStars : MonoBehaviour
{



    void Update()
    {
        transform.position = Lander.Instance.transform.position + new Vector3(15f, 15f, 0f);
    }
}
