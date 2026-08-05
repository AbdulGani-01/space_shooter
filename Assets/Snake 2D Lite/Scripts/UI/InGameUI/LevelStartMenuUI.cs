using System.Collections;
using UnityEngine;

namespace CodeFrontGames.Snake2DLite
{
    public class LevelStartMenuUI : MonoBehaviour
    {


        [SerializeField] private float delay = 2f;

        
        // Update is called once per frame
        void Update()
        {
            delay -= Time.unscaledDeltaTime;
            if (delay < 0)
            {
                GameManager.Instance.SetState(GameManager.State.Playing);
                gameObject.SetActive(false);
            }
        }
    }
}