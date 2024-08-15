using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class ContainerTileFactory : TileFactoryBase
    {
        [SerializeField] private int boardSizeX = 9;
        [SerializeField] private int boardSizeY = 10;
        [SerializeField] private List<TileScriptableObject> tileScriptableObjects;
        [SerializeField] private BoardData boardData;
        [SerializeField] private GameStateController gameStateController;
        [SerializeField] private MatchChecker matchChecker;

        private async void Start()
        {
            await Task.Delay(100);
            await BoardCreation();
            SetAllNeighbors();

            transform.position -= new Vector3((boardSizeX - 1f) / 2f, (boardSizeY - 1f) / 2f, 0f);
        }

        /// Board creation method. Time complexity: O(n*m)
        private async Task BoardCreation()
        {
            

            for (int y = 0; y < boardSizeY; y++)
                for (int x = 0; x < boardSizeX; x++)
                {
                    ContainerTile tile = GetProduct(new Vector3(x, y, 0), tileScriptableObjects[(x + y) % 2], transform) as ContainerTile;
                    tile.SetBoardPosition(new Vector2Int(x, y));
                    boardData.AddContainer(new Vector2Int(x, y), tile);
                    tile.MatchChecker = matchChecker;
                    tile.GetComponent<MoveInput>().MatchChecker = matchChecker;
                }

            await Task.CompletedTask;
        }

        /// Traverse through all containers to assign their neighbors. Time Complexity: O(n)
        private void SetAllNeighbors()
        {
            foreach (var i in boardData.Containers.Keys)
            {
                FindNeighbors(i, boardData.Containers[i]);
            }
        }

        /// Finds neighbor container tiles. Neighbor tiles will be used for match checks. 
        void FindNeighbors(Vector2Int boardPosition, ContainerTile containerTile)
        {
            if (boardData.Containers.ContainsKey(boardPosition + Vector2Int.right))
                containerTile.neighborTiles.Add(Vector2Int.right, boardData.Containers[boardPosition + Vector2Int.right]);
            else
                containerTile.neighborTiles.Add(Vector2Int.right, null);

            if (boardData.Containers.ContainsKey(boardPosition + Vector2Int.up))
                containerTile.neighborTiles.Add(Vector2Int.up, boardData.Containers[boardPosition + Vector2Int.up]);
            else
                containerTile.neighborTiles.Add(Vector2Int.up, null);

            if (boardData.Containers.ContainsKey(boardPosition + Vector2Int.left))
                containerTile.neighborTiles.Add(Vector2Int.left, boardData.Containers[boardPosition + Vector2Int.left]);
            else
                containerTile.neighborTiles.Add(Vector2Int.left, null);

            if (boardData.Containers.ContainsKey(boardPosition + Vector2Int.down))
                containerTile.neighborTiles.Add(Vector2Int.down, boardData.Containers[boardPosition + Vector2Int.down]);
            else
                containerTile.neighborTiles.Add(Vector2Int.down, null);
        }
    }
}