using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerLocomotionInput _playerLocomotionInput;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float autoMoveSpeed = 2f;
    [SerializeField] public float minY = -9f;
    [SerializeField] public float maxY = 9f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }

    private void FixedUpdate()
    {
        float moveX = autoMoveSpeed;
        float moveY = -_playerLocomotionInput.MovementInput * moveSpeed;
        
        if (_playerLocomotionInput.MovementInput == 0)
        {
            float newY = Mathf.MoveTowards(transform.position.y, 0, Time.fixedDeltaTime * moveSpeed * 1);
            
            transform.position = new Vector2(transform.position.x, newY);
            
            if (Mathf.Abs(transform.position.y) < 0.01f)
            {
                transform.position = new Vector2(transform.position.x, 0);
                moveY = 0;
            }
        }
        _rb.linearVelocity = new Vector2(moveX, moveY);
        
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector2(transform.position.x, clampedY);
    }

}