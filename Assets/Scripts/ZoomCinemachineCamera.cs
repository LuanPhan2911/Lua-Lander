using Cinemachine;
using UnityEngine;

public class ZoomCinemachineCamera : MonoBehaviour
{

    private const float NORMAL_ZOOM_SIZE = 12f;
    private float zoomSize;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    public static ZoomCinemachineCamera Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {

        float zoomSpeed = 5f;
        cinemachineVirtualCamera.m_Lens.OrthographicSize =
            Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, zoomSize, zoomSpeed * Time.deltaTime);
    }

    public void SetZoomOutSize(float zoomOutSize)
    {
        zoomSize = zoomOutSize;
    }


    public void SetNormalZoomSize()
    {

        zoomSize = NORMAL_ZOOM_SIZE;
    }
}
