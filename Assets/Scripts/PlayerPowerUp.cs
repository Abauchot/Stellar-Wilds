using System;
using UnityEngine;
using System.Collections;

public class PlayerPowerUp : MonoBehaviour
{

    private Rigidbody2D _rb;
    public bool ignoreGravity = false;
    public bool isShielded = false;
    private BackgroundLoop _backgroundLoop;
    private float _originalScrollSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _backgroundLoop = FindFirstObjectByType<BackgroundLoop>();
        if(_backgroundLoop != null)
            _originalScrollSpeed = _backgroundLoop.scrollSpeed;

    }
    
    public void ActivatePowerUp(PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUpType.SlowDown:
                StartCoroutine(SlowDownPowerUp(duration));
                break;
            case PowerUpType.Shield:
                StartCoroutine(ShieldPowerUp(duration));
                break;
            case PowerUpType.StableOrbit:
                StartCoroutine(StableOrbitPowerUp(duration));
                break;
        }
    }
    
    
    private IEnumerator SlowDownPowerUp(float duration)
    {
        _backgroundLoop.scrollSpeed *= 0.5f; 
        yield return new WaitForSeconds(duration);
        _backgroundLoop.scrollSpeed = _originalScrollSpeed;
    }

    private IEnumerator ShieldPowerUp(float duration)
    {
        isShielded = true;
        yield return new WaitForSeconds(duration);
        isShielded = false;
    }
    
    private IEnumerator StableOrbitPowerUp(float duration)
    {
        ignoreGravity = true;
        yield return new WaitForSeconds(duration);
        ignoreGravity = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlanetGravity>() && !isShielded)
        {
            PlayerCollision collision = GetComponent<PlayerCollision>();
            if(collision != null)
                collision.enabled = true;
        }
        else if(other.GetComponent<PlanetGravity>() && isShielded)
        {
        }
    }
    
    public bool IsShielded()
    {
        return isShielded;
    }
}

