using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private bool isGameOver = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called");
        Debug.Log($"Collision with {other.name}");
       
        if (other.GetComponent<PlanetGravity>() != null && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("💥 Collision avec une planète ! GAME OVER");

            
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            
            Invoke(nameof(StopGame), 1f);
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0f; // Met le jeu en pause
        Debug.Log("🔴 Game Over");
    }
}