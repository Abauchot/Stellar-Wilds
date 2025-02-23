using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private bool isGameOver = false;
    private PlayerPowerUp powerUp;
    
    private void Start()
    {
        powerUp = GetComponent<PlayerPowerUp>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlanetGravity>())
        {
            if (powerUp != null && powerUp.IsShielded()) 
            {
                Debug.Log("🛡️ Shield blocked the collision!");
                return; 
            }

            if (!isGameOver)
            {
                isGameOver = true;
                Debug.Log("💥 Collision avec une planète ! GAME OVER");
                GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                Invoke(nameof(StopGame), 1f);
            }
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0f; 
        Debug.Log("🔴 Game Over");
    }
}