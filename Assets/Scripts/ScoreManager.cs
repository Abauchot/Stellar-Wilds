using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private float startXPosition;
    [SerializeField] private float score = 0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startXPosition = player.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = player.position.x - startXPosition;
        float time = Time.timeSinceLevelLoad;

        score = distance * (1 + time / 0.1f); 
    }
    
    public float GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
