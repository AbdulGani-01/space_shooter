using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;



namespace CodeFrontGames.Snake2DLite
{
    /// <summary>
    /// Handles the overall game state logic such as pausing, winning, and losing the game.
    /// Also manages difficulty, score tracking, and scene transitions.
    /// </summary>
    public class GameManager : MonoBehaviour
    {


        // --- Events ---
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State state; // Holds the current game state
        }
        public event EventHandler OnGameLost;  // Triggered when the game is lost


        // --- Singleton Instance ---
        public static GameManager Instance { get; private set; }

        // --- Game States ---
        public enum State
        {
            Paused,
            StartDelay,
            Playing,
            GameLost
        }


        // --- Private Variables ---
        private State state;           // Current state of the game



        private void Awake()
        {
            // Setup singleton instance
            if (Instance == null)
            {
                Instance = this;
            }

            // Initialize default game state
            state = State.StartDelay;

            // Subscribe to relevant events
            OnStateChanged += GameStateManager_OnStateChanged;
        }

        private void Start()
        {
            // Ensure the game runs in real time at start
            Time.timeScale = 1f;
        }


        /// <summary>
        /// Called when the game state changes.
        /// Handles corresponding state transitions (pause, play, win, lose).
        /// </summary>
        private void GameStateManager_OnStateChanged(object sender, OnStateChangedEventArgs e)
        {
            if (e.state == State.Paused)
            {
                PauseGame();
            }
            else if (e.state == State.Playing)
            {
                PlayGame();
            }
            else if (e.state == State.GameLost)
            {
                GameLost();
            }
        }


        // --- Game Control Methods --- 
        private void PlayGame()
        {
            Time.timeScale = 1;
        }


        private void PauseGame()
        {
            Time.timeScale = 0;
        }


        // --- Utility Methods ---
        /// <summary>
        /// Returns true if the game is currently in the Playing state.
        /// </summary>
        public bool IsGamePlaying()
        {
            return state == State.Playing;
        }


        /// <summary>
        /// Sets the current game state and triggers the state changed event.
        /// </summary>
        public void SetState(State state)
        {
            this.state = state;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
            {
                state = state
            });
        }


        /// <summary>
        /// Gets the current game state.
        /// </summary>
        public State GetState()
        {
            return state;
        }

        private void GameLost()
        {
            PauseGame();

            AudioManager.Instance.Play(AudioManager.Sounds.GameLost.ToString());
            OnGameLost?.Invoke(this, EventArgs.Empty);
        }

    }
}
