using UnityEngine;
using System.Collections.Generic;


namespace CodeFrontGames.Snake2DLite
{
    /// <summary>
    /// Helper class to serialize key-value pairs of direction names and sprites
    /// for use in the inspector.
    /// </summary>
    [System.Serializable]
    public class SpriteKeyValuePair
    {
        public string key;
        public Sprite value;
    }


    /// <summary>
    /// Represents a single segment of the snake (head, tail, or body).
    /// Handles movement, direction changes, and sprite updates.
    /// </summary>
    public class SnakeBodyPart : MonoBehaviour
    {


        [SerializeField] private bool isHead;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color tintColor = Color.white;


        private Vector3Int direction = Vector3Int.zero;
        private Vector3Int cell = Vector3Int.zero;


        /// <summary>
        /// Moves the segment in the current direction and wraps if necessary.
        /// </summary>
        public void Move()
        {
            cell += direction;
            cell = GridManager.Instance.WrapPosition(cell);
        }

        /// <summary>
        /// Sets the segment to a specific cell position.
        /// </summary>
        public void SetPos(Vector3Int cell)
        {
            this.cell = GridManager.Instance.WrapPosition(cell);
        }

        /// <summary>
        /// Updates the movement direction and determines if body should curve.
        /// </summary>
        public void SetDirection(Vector3Int newDir)
        {
            direction = newDir;
        }

        /// <summary>
        /// Returns the current movement direction.
        /// </summary>
        public Vector3Int GetDirection()
        {
            return direction;
        }

        /// <summary>
        /// Returns the current grid cell.
        /// </summary>
        public Vector3Int GetCurrentCell()
        {
            return cell;
        }

        /// <summary>
        /// Returns the sprite used for this segment.
        /// </summary>
        public Sprite GetSprite()
        {
            return sprite;
        }

        public Color GetColor()
        {
            return tintColor;
        }

        public bool IsHead()
        {
            return isHead;
        }
    }
}
