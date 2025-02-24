using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public Sprite[] planetSprites;
    [SerializeField] public float gravityStrength = 5f;
    [SerializeField] public float gravityRadius = 5f;
    [SerializeField] public float moveSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignRandomSprite();
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.left *( moveSpeed * Time.fixedDeltaTime);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ApplyGravity(player);
        }
    }

    private void ApplyGravity(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        PlayerPowerUp powerUp = player.GetComponent<PlayerPowerUp>();
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (rb == null || playerController == null || 
            (powerUp != null && powerUp.ignoreGravity) || 
            playerController.HasGravityImmunity())
            return;

        Vector2 direction = (transform.position - player.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < gravityRadius)
        {
            if (distance < gravityRadius * 0.5f && !playerController.IsOrbiting())
            {
                playerController.StartOrbit(transform);
            }
            else if (!playerController.IsOrbiting())
            {
                float forceMagnitude = gravityStrength * (1 - (distance / gravityRadius));
                forceMagnitude = Mathf.Clamp(forceMagnitude, 0, gravityStrength);
                rb.AddForce(direction * forceMagnitude, ForceMode2D.Force);
            }
        }
    }



    private void Update()
    {
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
    
    private void AssignRandomSprite()
    {
        if (planetSprites.Length > 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = planetSprites[Random.Range(0, planetSprites.Length)];
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}