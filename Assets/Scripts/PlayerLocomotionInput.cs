using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
    public PlayerControls PlayerControls { get; private set; }
    public float  MovementInput { get; private set; }

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

        Debug.Log($"Input reçu : {MovementInput}, Phase: {context.phase}");

        if (context.canceled)
        {
            MovementInput = 0f;
            Debug.Log("Input relâché, remise à zéro.");
        }
    }

}
