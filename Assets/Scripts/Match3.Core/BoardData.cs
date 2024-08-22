using System.Collections.Generic;
using UnityEngine;

namespace Match3.Core
{
    public class BoardData
    {
        public Dictionary<Vector2Int, PlayTile> boardDataDictionary = new Dictionary<Vector2Int, PlayTile>();  //Stores board positions and tile types

        Vector3 offset;

        public void SetTile(Vector2Int boardPosition, PlayTile playTile)
        {
            boardDataDictionary[boardPosition] = playTile;
        }

        public void SetOffset()
        {
            offset = boardDataDictionary[Vector2Int.zero].transform.position;
        }

        public PlayTile GetTileData(Vector2Int boardPosition)
        {
            if (!boardDataDictionary.ContainsKey(boardPosition))
                return null;

            return boardDataDictionary[boardPosition];
        }

        public Vector3 GetWorldPosition(Vector2Int boardPosition)
        {
            return new Vector3(boardPosition.x + offset.x, boardPosition.y + offset.y, 0f);
        }
    }
}