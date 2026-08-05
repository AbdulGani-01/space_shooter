using UnityEngine;
using UnityEngine.UI;

namespace CodeFrontGames.Snake2DLite
{
    public class GameResultUI : MonoBehaviour
    {


        [SerializeField] private ButtonHandlerUI buttonHandlerUI;
        [SerializeField] private Image backgroundUI;
        [SerializeField] private GameObject gameLostUI;


        private void Start()
        {
            GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        }

        private void Instance_OnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
        {
            if (e.state == GameManager.State.GameLost)
            {
                buttonHandlerUI.GameOver();
                backgroundUI.gameObject.SetActive(true);
                gameLostUI.SetActive(true);
            }
        }
    }
}
