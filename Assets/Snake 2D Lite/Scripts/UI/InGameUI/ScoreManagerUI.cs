using TMPro;
using UnityEngine;

namespace CodeFrontGames.Snake2DLite
{
    public class ScoreManagerUI : MonoBehaviour
    {


        [SerializeField] private TextMeshProUGUI scoreText;


        private int score;


        private void Awake()
        {
            Food.onScoreChanged += Food_onScoreChanged;
        }

        private void Food_onScoreChanged(object sender, Food.OnScoreChangedEvenArgs e)
        {
            score += e.score;

            SetScore();
        }

        private void SetScore()
        {
            scoreText.text = score.ToString();
        }
    }
}

