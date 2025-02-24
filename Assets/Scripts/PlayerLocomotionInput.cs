using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
    public PlayerControls PlayerControls { get; private set; }
    public float  MovementInput { get; private set; }
    public bool EscapeOrbit { get;  set; }

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();
        PlayerControls.PlayerLocomotionMap.Enable();
        PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.PlayerLocomotionMap.Disable();
        PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<float>();
        if (context.canceled)
        {
            MovementInput = 0f;
        }
    }
    
    public void OnEscapeOrbit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EscapeOrbit = true;
            Debug.Log("🚀 Escape Orbit");
        }
    }

}
