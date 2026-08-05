using System;
using System.Collections.Generic;
using UnityEngine;


namespace CodeFrontGames.Snake2DLite
{
    /// <summary>
    /// Handles all logic for the snake — including movement, growth, collision detection,
    /// and integration with other systems like Food, GridManager, and GameManager.
    /// </summary>
    public class SnakeManager : MonoBehaviour
    {

        // --- Singleton Instance ---
        public static SnakeManager Instance { get; private set; }


        // --- Serialized Fields (Configurable in Inspector) ---
        [SerializeField] private SnakeBodyPart head;        // Reference to the snake's head segment
        [SerializeField] private SnakeBodyPart snakeBody;   // Prefab used to spawn new body parts
        [SerializeField] private Vector3Int startCell = Vector3Int.zero;   // Starting grid position of the snake
        [SerializeField] private float moveSpeed = 1f;      // Movement speed multiplier


        // --- Private Runtime Variables ---
        private Vector3Int direction = Vector3Int.zero;       // Current movement direction
        private List<SnakeBodyPart> snakeBodyPartsList;       // All body parts (head, body, tail)
        private float stepTimer = 1f;                         // Countdown timer for movement steps
        private bool justTurned = false;                      // Prevents multiple turns between movement steps
        private Vector3Int lastBodyPartDirection;
        private Vector3Int lastBodyPartCell;


        // --- Unity Lifecycle Methods ---
        private void Awake()
        {
            // Set up singleton pattern so other scripts can easily access this instance
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            // Initialize the snake's body list and set starting positions
            snakeBodyPartsList = new List<SnakeBodyPart>();
            head.SetPos(startCell);

            // Add head and tail to the list (initially just two parts)
            snakeBodyPartsList.Add(head);

            // Update the grid to reflect the snake’s starting position
            GridManager.Instance.UpdateSnakeTiles();

            // Subscribe to the food eaten event to grow the snake
            Food.onFoodEaten += Food_foodEaten;
        }

        /// <summary>
        /// Called when the snake eats food. Adds a new body part between the tail and the last segment.
        /// </summary>
        private void Food_foodEaten(object sender, EventArgs e)
        {
            // Instantiate a new snake body part from the prefab
            SnakeBodyPart newSnakeBody = Instantiate<SnakeBodyPart>(snakeBody);

            newSnakeBody.SetPos(lastBodyPartCell);

            newSnakeBody.SetDirection(lastBodyPartDirection);

            // Insert the new body part before the tail
            snakeBodyPartsList.Add(newSnakeBody);
        }

        // --- Update Loop ---
        private void Update()
        {
            if (GameManager.Instance.GetState() == GameManager.State.Playing)
            {
                // Handle player input and update movement direction
                GetDirection();

                // Store old tail data for use if the snake grows
                SnakeBodyPart lastSnakeBodyPart = snakeBodyPartsList[snakeBodyPartsList.Count - 1];
                lastBodyPartCell = lastSnakeBodyPart.GetCurrentCell();
                lastBodyPartDirection = lastSnakeBodyPart.GetDirection();

                // Apply direction to head
                head.SetDirection(direction);

                // Move snake when the step timer reaches zero
                if (stepTimer <= 0f)
                {

                    head.Move();
                    

                    // Move each body part from tail to head
                    for (int i = snakeBodyPartsList.Count - 1; i >= 1; i--)
                    {
                        snakeBodyPartsList[i].Move();
                        snakeBodyPartsList[i].SetDirection(snakeBodyPartsList[i - 1].GetDirection());
                    }

                    // Check For Head Collision With Food
                    CheckForFoodCollisons(head.GetCurrentCell());

                    if (CheckForSelfCollisons(head.GetCurrentCell()))
                    {

                        GameManager.Instance.SetState(GameManager.State.GameLost);
                        return;
                    }

                    // Update grid visuals and logic
                    GridManager.Instance.UpdateSnakeTiles();

                    // Reset turn and timer
                    justTurned = false;
                    stepTimer = 1f;
                }
                else
                {
                    // Countdown to next movement step (scaled by move speed)
                    stepTimer -= (Time.deltaTime * moveSpeed);
                }
            }
        }

        private bool CheckForSelfCollisons(Vector3Int cell)
        {
            if (GridManager.Instance.IsCellOccupied(cell) && direction != Vector3Int.zero)
            {
                if (GridManager.Instance.IsCellSnake(cell))
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            return false;
        }

        private void CheckForFoodCollisons(Vector3Int cell)
        {
            // Only check collisions if snake is moving
            if (GridManager.Instance.IsCellOccupied(cell) && direction != Vector3Int.zero)
            {
                // If cell contains food, trigger the food-eaten event
                if (GridManager.Instance.IsCellFood(cell, out Food food))
                {
                    food.FoodEaten();
                }
            }
        }

        /// <summary>
        /// Updates the movement direction based on player input.
        /// Ensures only one turn per movement step.
        /// </summary>
        private void GetDirection()
        {
            if (!justTurned && GameManager.Instance.IsGamePlaying())
            {
                // Update direction only if input is different from current movement
                if (direction != GameInputManager.Instance.MoveDirection)
                {
                    direction = GameInputManager.Instance.MoveDirection;
                    justTurned = true;
                }
            }
        }

        // --- Public Utility Methods ---

        /// <summary>
        /// Returns a list of all snake body parts (head, body, tail).
        /// Useful for grid updates or collision checks.
        /// </summary>
        public List<SnakeBodyPart> GetSnakeBodyPartList()
        {
            return snakeBodyPartsList;
        }

    }
}
