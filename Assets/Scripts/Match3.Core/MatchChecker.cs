using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class MatchChecker: ICommand
    {
        [Inject]BoardData boardData;

        public async Task<HashSet<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates)
        {
            HashSet<Vector2Int> blastTileCoordinates = new HashSet<Vector2Int>();

            var tasks = new List<Task<HashSet<Vector2Int>>>();
            foreach (Vector2Int coordinate in coordinates)
            {
                tasks.Add(MatchCheck(coordinate, Vector2Int.up, Vector2Int.down));
                tasks.Add(MatchCheck(coordinate, Vector2Int.right, Vector2Int.left));
            }

            var results = await Task.WhenAll(tasks);
            foreach (var result in results)
            {
                blastTileCoordinates.UnionWith(result);
            }

            return blastTileCoordinates;
        }

        async Task<HashSet<Vector2Int>> MatchCheck(Vector2Int coordinate, Vector2Int direction1, Vector2Int direction2)
        {
            Stack<Vector2Int> stack = new Stack<Vector2Int>();
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            HashSet<Vector2Int> blastCoordinates = new HashSet<Vector2Int>();

            stack.Push(coordinate);

            await Task.Run(() =>
            {
                while (stack.Count != 0)
                {
                    Vector2Int current = stack.Pop();
                    blastCoordinates.Add(current);

                    if (visited.Contains(current))
                        continue;

                    if (boardData.BoardDataDictionary.ContainsKey(current + direction1)
                        && boardData.GetTileData(current).TileID == boardData.GetTileData(current + direction1).TileID)
                        stack.Push(current + direction1);

                    if (boardData.BoardDataDictionary.ContainsKey(current + direction2)
                        && boardData.GetTileData(current).TileID == boardData.GetTileData(current + direction2).TileID)
                        stack.Push(current + direction2);

                    visited.Add(current);
                }
            });

            if (blastCoordinates.Count >= 3)
                return blastCoordinates;

            return new HashSet<Vector2Int>();
        }
    }
}