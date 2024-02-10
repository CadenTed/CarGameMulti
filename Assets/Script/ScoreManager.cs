using TMPro;
using UnityEngine;

// Score Manager

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int highScore = 0;

    public static int scorePass = 0;

    public TextMeshProUGUI scoreText;

    public static ScoreManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Increase Score
    public void AddScore(int value)
    {
        score += value;
        scorePass = score;
        scoreText.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    // Save score
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
