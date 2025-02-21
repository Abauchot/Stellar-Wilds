using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerLocomotionInput _playerLocomotionInput;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float autoMoveSpeed = 2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }

    private void FixedUpdate()
    {
        float moveX = autoMoveSpeed;
        float moveY = -_playerLocomotionInput.MovementInput * moveSpeed;
        _rb.linearVelocity = new Vector2(moveX, moveY);
    }
}