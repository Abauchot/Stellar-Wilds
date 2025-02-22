using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [SerializeField] public float gravityStrength = 5f;
    [SerializeField] public float gravityRadius;
    [SerializeField] float moveSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityRadius = GetComponent<CircleCollider2D>().radius * 3 * transform.localScale.x;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ApplyGravity(player);
        }
    }

    private void ApplyGravity(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            return;
        }

        Vector2 direction = (transform.position - player.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < gravityRadius)
        {
            float forceMagnitude = gravityStrength / (distance * distance);
            rb.AddForce(direction * forceMagnitude, ForceMode2D.Force);
        }
    }

    private void Update()
    {
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}