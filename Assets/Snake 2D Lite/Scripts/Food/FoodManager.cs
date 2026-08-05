using UnityEngine;
using System.Collections.Generic;


namespace CodeFrontGames.Snake2DLite
{
    /// <summary>
    /// Controls spawning, tracking, and removal of food objects in the game.
    /// Ensures food count stays within the defined limit.
    /// </summary>
    public class FoodManager : MonoBehaviour
    {


        public static FoodManager Instance { get; private set; }


        [SerializeField] private Food food;
        [SerializeField] private int maxFood;
        [SerializeField] private float foodSpawnDelay;


        private List<Food> foodList = new();             // Active food instances
        private float foodSpawnTimer;                    // Countdown for next spawn

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            foodSpawnTimer = foodSpawnDelay;

            // Subscribe to food-eaten event to remove eaten food
            Food.onFoodEaten += Food_foodEaten;
        }

        /// <summary>
        /// Handles cleanup when a food item is eaten.
        /// </summary>
        private void Food_foodEaten(object sender, System.EventArgs e)
        {
            Food eatenFood = sender as Food;
            foodList.Remove(eatenFood);

            GridManager.Instance.UpdateFoodTiles();
        }

        private void Update()
        {
            // Only spawn new food if under the limit
            if (foodList.Count < maxFood)
            {
                foodSpawnTimer -= Time.deltaTime;

                if (foodSpawnTimer < 0)
                {
                    SpawnFood();
                }
            }
        }

        /// <summary>
        /// Instantiates a new food item at a random free grid cell.
        /// </summary>
        public void SpawnFood()
        {
            foodSpawnTimer = foodSpawnDelay;

            Food newFood = Instantiate<Food>(food);
            newFood.SetCell(GetNewFoodCell());

            foodList.Add(newFood);
            GridManager.Instance.UpdateFoodTiles();
        }

        /// <summary>
        /// Finds an unoccupied random cell for spawning new food.
        /// </summary>
        private Vector3Int GetNewFoodCell()
        {
            Vector3Int newFoodCell = Vector3Int.zero;
            newFoodCell.x = Random.Range(-GridManager.Instance.LeftCellAmount, GridManager.Instance.RightCellAmount);
            newFoodCell.y = Random.Range(-GridManager.Instance.DownCellAmount, GridManager.Instance.UpCellAmount);

            // Recursively retry if the cell is already occupied
            if (GridManager.Instance.IsCellOccupied(newFoodCell))
            {
                newFoodCell = GetNewFoodCell();
            }

            return newFoodCell;
        }

        /// <summary>
        /// Returns the list of all active food items.
        /// </summary>
        public List<Food> GetFoodList()
        {
            return foodList;
        }
    }
}
