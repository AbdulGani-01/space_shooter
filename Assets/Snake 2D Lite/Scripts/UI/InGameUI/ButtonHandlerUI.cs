using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace CodeFrontGames.Snake2DLite
{
    public class ButtonHandlerUI : MonoBehaviour
    {


        [SerializeField] private Button QuitButtonUI;
        [SerializeField] private Button GamePauseButtonUI;
        [SerializeField] private Button GameResumeButtonUI;
        [SerializeField] private Button GameRestartButtonUI;


        private void Awake()
        {
            QuitButtonUI.onClick.AddListener(() =>
            {
                Application.Quit();
            });


            GamePauseButtonUI.onClick.AddListener(() =>
            {
                GameManager.Instance.SetState(GameManager.State.Paused);

                GameResumeButtonUI.gameObject.SetActive(true);
                GamePauseButtonUI.gameObject.SetActive(false);
            });


            GameResumeButtonUI.onClick.AddListener(() =>
            {
                GameManager.Instance.SetState(GameManager.State.Playing);

                GamePauseButtonUI.gameObject.SetActive(true);
                GameResumeButtonUI.gameObject.SetActive(false);
            });


            GameRestartButtonUI.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }


        public void GameOver()
        {
            GamePauseButtonUI.gameObject.SetActive(false);
            GameResumeButtonUI.gameObject.SetActive(false);
        }


    }
}
