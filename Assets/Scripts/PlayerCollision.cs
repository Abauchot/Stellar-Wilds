using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isGameOver = false;
    private PlayerPowerUp powerUp;
    private GameManager gameManager; // ajout de la référence

    private void Start()
    {
        powerUp = GetComponent<PlayerPowerUp>();
        gameManager = FindFirstObjectByType<GameManager>(); // référence automatique au GameManager
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlanetGravity>())
        {
            if (powerUp != null && powerUp.IsShielded()) 
            {
                return; 
            }

            if (!isGameOver)
            {
                isGameOver = true;
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                Invoke(nameof(StopGame), 1f);
            }
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0f; 
        gameManager.GameOver(); // Appel clair du GameOver()
    }
}