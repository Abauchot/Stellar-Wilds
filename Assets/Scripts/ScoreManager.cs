using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceMultiplier = 0.1f; // ajuste selon l'équilibrage souhaité
    private float startXPosition;
    private float score;

    void Start()
    {
        startXPosition = player.position.x;
    }

    void Update()
    {
        float distanceTraveled = player.position.x - startXPosition;
        if (distanceTraveled > 0)
        {
            score = Mathf.Max(score, distanceTraveled * distanceMultiplier);
        }
    }
    public string GetScoreInLightYears()
    {
        return $"{score:F2} light-years";
    }
}