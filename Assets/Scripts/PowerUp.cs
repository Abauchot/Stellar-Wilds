using System;
using UnityEngine;

public enum PowerUpType
{
    SlowDown,
    Shield,
    StableOrbit
}
public class PowerUp : MonoBehaviour
{
    public PowerUpType type; 
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPowerUp player = other.GetComponent<PlayerPowerUp>();
        if (player != null)
        {
            player.ActivatePowerUp(type, duration);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * 2f));
        
    }
}
