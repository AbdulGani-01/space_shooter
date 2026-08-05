using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeFrontGames.Snake2DLite
{
    public class ButtonUI : MonoBehaviour, IPointerDownHandler
    {

        void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            GetComponent<Button>().onClick.Invoke();

            AudioManager.Instance.Play(AudioManager.Sounds.ButtonPress.ToString());
        }

    }
}
