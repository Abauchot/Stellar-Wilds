using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject retryButton;
    public GameObject quitButton;

    private bool isGameOver = false;
    
    void Start()
    {
        MusicManager.instance.PlayMusic();
    }


    private void Update()
    {
        if (!isGameOver)
        {
            scoreText.text = $"Distance: {scoreManager.GetScoreInLightYears()}";
        }
    }


    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}