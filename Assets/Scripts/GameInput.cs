using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }
    private InputSystem inputSystem;

    private void Awake()
    {
        Instance = this;
        inputSystem = new InputSystem();

        inputSystem.Enable();
    }

    private void OnDestroy()
    {
        inputSystem.Disable();
    }

    public bool IsLanderUp()
    {
        return inputSystem.Player.LanderUp.IsPressed();
    }

    public bool IsLanderLeft()
    {
        return inputSystem.Player.LanderLeft.IsPressed();
    }
    public bool IsLanderRight()
    {
        return inputSystem.Player.LanderRight.IsPressed();

    }
}
