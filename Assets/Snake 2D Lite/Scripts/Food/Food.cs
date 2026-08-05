using System;
using UnityEngine;


namespace CodeFrontGames.Snake2DLite
{
    /// <summary>
    /// Represents a food object that the snake can eat.
    /// Handles score increase, sound playback, and destruction when eaten.
    /// </summary>
    public class Food : MonoBehaviour
    {
        // --- Static Events ---
        public static event EventHandler onFoodEaten; // Triggered when food is eaten
        public static event EventHandler<OnScoreChangedEvenArgs> onScoreChanged; // Triggered when score changes

        public class OnScoreChangedEvenArgs : EventArgs
        {
            public int score; // Score value granted by this food
        }


        [SerializeField] private Sprite sprite;
        [SerializeField] private Color tintColor = Color.red;
        [SerializeField] private int score;


        private Vector3Int cell; // Grid cell position of this food

        /// <summary>
        /// Called when the food is eaten by the snake.
        /// Triggers events, plays sound, and destroys itself.
        /// </summary>
        public void FoodEaten()
        {
            onFoodEaten?.Invoke(this, EventArgs.Empty);
            onScoreChanged?.Invoke(this, new OnScoreChangedEvenArgs
            {
                score = score
            });

            AudioManager.Instance.Play(AudioManager.Sounds.FoodEaten.ToString());
            Destroy(gameObject);
        }

        /// <summary>
        /// Returns the grid cell position of this food.
        /// </summary>
        public Vector3Int GetCell()
        {
            return cell;
        }

        /// <summary>
        /// Sets the grid cell position and updates world position.
        /// </summary>
        public void SetCell(Vector3Int cell)
        {
            this.cell = cell;
            transform.position = new Vector3(cell.x, cell.y, 0f);
        }

        /// <summary>
        /// Returns the food’s sprite.
        /// </summary>
        public Sprite GetSprite()
        {
            return sprite;
        }

        public Color GetColor()
        {
            return tintColor;
        }
    }
}
