using System.Collections.Generic;
using UnityEngine;

namespace Match3.Core
{
    public class BoardData
    {
        private Dictionary<Vector2Int, PlayTile> boardData = new Dictionary<Vector2Int, PlayTile>();  //Stores board positions and tile types (as integers)
        public IReadOnlyDictionary<Vector2Int, PlayTile> BoardDataDictionary { get { return boardData; } }

        Vector3 offset;

        public void SetTile(Vector2Int boardPosition, PlayTile playTile)
        {
            boardData[boardPosition] = playTile;
        }

        public void SetOffset()
        {
            offset = boardData[Vector2Int.zero].transform.position;
        }

        public PlayTile GetTileData(Vector2Int boardPosition)
        {
            if (!boardData.ContainsKey(boardPosition))
                return null;

            return boardData[boardPosition];
        }

        public Vector3 GetWorldPosition(Vector2Int boardPosition)
        {
            return new Vector3(boardPosition.x + offset.x, boardPosition.y + offset.y, 0f);
        }
    }
}