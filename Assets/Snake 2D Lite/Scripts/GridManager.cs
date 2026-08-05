using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.Tilemaps.Tile;


namespace CodeFrontGames.Snake2DLite
{
    /// <summary>
    /// Handles grid-related logic such as placing, updating, and checking tiles
    /// for snake, food, and obstacles. Also enforces grid bounds and wrapping.
    /// </summary>
    public class GridManager : MonoBehaviour
    {


        public static GridManager Instance { get; private set; }

        [SerializeField, Range(0, 12)] private int leftCellAmount;
        [SerializeField, Range(0, 12)] private int rightCellAmount;
        [SerializeField, Range(0, 10)] private int upCellAmount;
        [SerializeField, Range(0, 10)] private int downCellAmount;


        // Public accessors for grid bounds
        public int LeftCellAmount => leftCellAmount;
        public int RightCellAmount => rightCellAmount;
        public int UpCellAmount => upCellAmount;
        public int DownCellAmount => downCellAmount;

        // Track active grid tiles
        private List<Vector3Int> snakeTileCells = new();
        private List<Vector3Int> foodTileCells = new();

        // Cache tiles for snake parts to reuse sprites
        private Dictionary<SnakeBodyPart, Tile> snakeCachedTilesDictionary = new();

        private Tilemap tileMap;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            tileMap = GetComponent<Tilemap>();
        }

        /// <summary>
        /// Updates all snake tiles on the grid.
        /// </summary>
        public void UpdateSnakeTiles()
        {
            // Clear old tiles
            foreach (Vector3Int tileCell in snakeTileCells)
                tileMap.SetTile(tileCell, null);

            snakeTileCells.Clear();

            // Draw each body part
            foreach (SnakeBodyPart bodyPart in SnakeManager.Instance.GetSnakeBodyPartList())
            {
                if (!snakeCachedTilesDictionary.ContainsKey(bodyPart))
                {
                    Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
                    tile.sprite = bodyPart.GetSprite();
                    tile.color = bodyPart.GetColor();
                    tile.transform = Matrix4x4.Scale(new Vector3(0.3f, 0.3f, 1f));

                    snakeCachedTilesDictionary.Add(bodyPart, tile);
                }
                Vector3Int cell = bodyPart.GetCurrentCell();

                tileMap.SetTile(cell, snakeCachedTilesDictionary[bodyPart]);
                snakeTileCells.Add(cell);
            }

            tileMap.RefreshAllTiles();
        }

        /// <summary>
        /// Updates all food tiles on the grid.
        /// </summary>
        public void UpdateFoodTiles()
        {
            foreach (Vector3Int tileCell in foodTileCells)
                tileMap.SetTile(tileCell, null);

            foodTileCells.Clear();

            foreach (Food food in FoodManager.Instance.GetFoodList())
            {
                Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
                tile.sprite = food.GetSprite();
                tile.color = food.GetColor();
                tile.transform = Matrix4x4.Scale(new Vector3(0.3f, 0.3f, 1f));

                tileMap.SetTile(food.GetCell(), tile);
                foodTileCells.Add(food.GetCell());
            }

            tileMap.RefreshAllTiles();
        }

        /// <summary>
        /// Returns true if a given cell already has a tile.
        /// </summary>
        public bool IsCellOccupied(Vector3Int cell)
        {
            return tileMap.GetTile(cell) != null;
        }

        /// <summary>
        /// Checks if a cell contains food, returning the Food object if found.
        /// </summary>
        public bool IsCellFood(Vector3Int cell, out Food outFood)
        {
            foreach (Food food in FoodManager.Instance.GetFoodList())
            {
                if (food.GetCell() == cell)
                {
                    outFood = food;
                    return true;
                }
            }

            outFood = null;
            return false;
        }

        public bool IsCellSnake(Vector3Int cell)
        {
            foreach (SnakeBodyPart snakeBodyPart in SnakeManager.Instance.GetSnakeBodyPartList())
            {
                if (snakeBodyPart.IsHead()) continue;
                if (snakeBodyPart.GetCurrentCell() == cell)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Wraps the position (for looping gameplay).
        /// </summary>
        public Vector3Int WrapPosition(Vector3Int cell)
        {
            if (cell.x > RightCellAmount)
                cell.x = -LeftCellAmount;
            else if (cell.x < -LeftCellAmount)
                cell.x = RightCellAmount;

            if (cell.y > UpCellAmount)
                cell.y = -DownCellAmount;
            else if (cell.y < -DownCellAmount)
                cell.y = UpCellAmount;
            

            return cell;
        }

    }
}