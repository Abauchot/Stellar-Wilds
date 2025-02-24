using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerLocomotionInput _playerLocomotionInput;

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float autoMoveSpeed = 2f;
    [SerializeField] public float minY = -9f;
    [SerializeField] public float maxY = 9f;

    private bool isOrbiting = false;
    private Transform currentPlanet;
    private float orbitSpeed = 200f;
    private float orbitRadius;
    private float orbitShrinkSpeed = 0.5f;
    private float orbitRadiusIncreasePerTap = 10f;
    private float orbitEscapeRadius = 5f;
    private bool gravityImmune = false;
    private float immunityDuration = 2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
    }

    private void FixedUpdate()
    {
        if (isOrbiting)
        {
            _rb.linearVelocity = Vector2.zero; 
            Orbit();
            return;
        }

        float moveX = autoMoveSpeed;
        float moveY = -_playerLocomotionInput.MovementInput * moveSpeed;

        if (_playerLocomotionInput.MovementInput == 0)
        {
            float newY = Mathf.MoveTowards(transform.position.y, 0, Time.fixedDeltaTime * moveSpeed);
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
    private void Update()
    {
        if (isOrbiting && _playerLocomotionInput.EscapeOrbit)
        {
            orbitRadius += orbitRadiusIncreasePerTap;
            orbitShrinkSpeed = 0f;

            Debug.Log($"Orbit radius: {orbitRadius}");

            if (orbitRadius >= orbitEscapeRadius)
            {
                isOrbiting = false;
                
                Vector2 escapeDirection = (transform.position - currentPlanet.position).normalized;
                float escapeForce = 10f; 
                _rb.AddForce(escapeDirection * escapeForce, ForceMode2D.Impulse);

                currentPlanet = null;
                orbitShrinkSpeed = 0.5f;
                
                StartCoroutine(GravityImmunityCoroutine());
            }

            _playerLocomotionInput.EscapeOrbit = false;
        }
    }

    public bool HasGravityImmunity() => gravityImmune;
    
    private IEnumerator GravityImmunityCoroutine()
    {
        gravityImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        gravityImmune = false;
    }



    private void Orbit()
    {
        if (currentPlanet == null) return;

        Vector2 direction = (transform.position - currentPlanet.position).normalized;

        float angle = orbitSpeed * Time.deltaTime;
        direction = Quaternion.Euler(0, 0, angle) * direction;

        orbitRadius -= orbitShrinkSpeed * Time.deltaTime;
        orbitRadius = Mathf.Clamp(orbitRadius, 0.3f, 10f);

        transform.position = currentPlanet.position + (Vector3)(direction * orbitRadius);
    }

    public void StartOrbit(Transform planet)
    {
        if (!isOrbiting)
        {
            isOrbiting = true;
            currentPlanet = planet;
            orbitRadius = Vector2.Distance(transform.position, planet.position);
            _rb.linearVelocity = Vector2.zero;
        }
    }

    public bool IsOrbiting() => isOrbiting;
}