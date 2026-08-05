using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour
{
    public TextMeshProUGUI StartText;
    public GameObject PlayButton;
    public TextMeshProUGUI GameOverText;
    public GameObject restartButton;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Total_score;
    public float score = 0;

    void Start()
    {
        Time.timeScale = 0f;
        StartText.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        Total_score.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Total_Score();
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        StartText.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Score_Count()
    {
        score++;
        Score.text = "" + score;
    }

    public void Total_Score()
    {
        Total_score.gameObject.SetActive(true);
        Total_score.text = "Score : " + score;
    }
}