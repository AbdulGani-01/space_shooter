using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeFrontGames.Snake2DLite
{
    public class GameInputManager : MonoBehaviour
    {


        public static GameInputManager Instance { get; private set; }

        public Vector3Int MoveDirection { get; private set; }

        private bool isSwiping = false;
        private Vector2 startTouchPos;
        private Vector2 currentTouchPos;
        private float swipeThreshold = 50f; // pixels

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Update()
        {
            // UNITY_STANDALONE || UNITY_EDITOR
            ReadKeyboardInput();
            // UNITY_ANDROID || UNITY_IOS
            ReadTouchInput();
            //
        }

        // ---------------------------------------------
        // PC / Keyboard input (WASD + Arrows)
        // ---------------------------------------------
        private void ReadKeyboardInput()
        {
            if (Keyboard.current == null) return;

            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
                SetDirection(Vector3Int.up);

            else if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
                SetDirection(Vector3Int.down);

            else if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
                SetDirection(Vector3Int.left);

            else if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
                SetDirection(Vector3Int.right);
        }

        // ---------------------------------------------
        // Mobile / Touch swipe input
        // ---------------------------------------------
        private void ReadTouchInput()
        {
            if (Touchscreen.current == null) return;

            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                startTouchPos = touch.position.ReadValue();
                isSwiping = true;
            }
            else if (touch.press.wasReleasedThisFrame)
            {
                currentTouchPos = touch.position.ReadValue();
                DetectSwipe();

                isSwiping = false;
            }

            if (isSwiping)
            {
                currentTouchPos = touch.position.ReadValue();
                DetectSwipe();
            }
        }

        private void DetectSwipe()
        {
            Vector2 delta = currentTouchPos - startTouchPos;
            if (delta.magnitude < swipeThreshold)
                return;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                // Horizontal swipe
                if (delta.x > 0)
                    SetDirection(Vector3Int.right);
                else
                    SetDirection(Vector3Int.left);
            }
            else
            {
                // Vertical swipe
                if (delta.y > 0)
                    SetDirection(Vector3Int.up);
                else
                    SetDirection(Vector3Int.down);
            }

            startTouchPos = currentTouchPos;
        }


        // ---------------------------------------------
        // Helper
        // ---------------------------------------------
        private void SetDirection(Vector3Int newDir)
        {
            // prevent 180° turn
            if (newDir + MoveDirection != Vector3Int.zero)
                MoveDirection = newDir;
        }


    }
}
