using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }
    private InputSystem inputSystem;

    public event EventHandler OnMenuButtonPressed;

    private void Awake()
    {
        Instance = this;
        inputSystem = new InputSystem();

        inputSystem.Enable();

        inputSystem.Player.Menu.performed += OnMenuPerformed;
    }

    private void OnMenuPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
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

    public Vector2 GetLanderMovement()
    {
        return inputSystem.Player.Movement.ReadValue<Vector2>();
    }
}
