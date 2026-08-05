using UnityEngine;


namespace CodeFrontGames.Snake2DLite
{
    [ExecuteAlways]
    public class BackgroundScaler : MonoBehaviour
    {

        private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
        private GridManager gridManager => FindAnyObjectByType<GridManager>();


        private void Update()
        {
            #if UNITY_EDITOR
                if (spriteRenderer == null || gridManager == null) return;

                Grid grid = gridManager.GetComponent<Grid>();
                if (grid == null) return;

                // --- Calculate width and height in world units ---
                float width = (gridManager.RightCellAmount + gridManager.LeftCellAmount + 1) * grid.cellSize.x;
                float height = (gridManager.UpCellAmount + gridManager.DownCellAmount + 1) * grid.cellSize.y;


                // Convert the center cell (midpoint between bounds) to world position
                float centerX = ((gridManager.RightCellAmount - gridManager.LeftCellAmount) / 2f * grid.cellSize.x) + grid.cellSize.x / 2;
                float centerY = ((gridManager.UpCellAmount - gridManager.DownCellAmount) / 2f * grid.cellSize.y) + grid.cellSize.y / 2;
                Vector3 centerWorld = grid.CellToWorld(Vector3Int.zero) + new Vector3(centerX, centerY, 0);

                // --- Get sprite’s original size ---
                Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

                // --- 4Scale the sprite to cover full grid + padding ---
                transform.localScale = new Vector3(
                    (width) / spriteSize.x,
                    (height) / spriteSize.y,
                    1
                );

                // --- Position it correctly ---
                transform.position = centerWorld;
            #endif
        }
    }
}
