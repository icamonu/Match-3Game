using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Match3.Core
{
    public class MatchChecker
    {
        public async Task<HashSet<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates, Dictionary<Vector2Int, PlayTile> boardDataDictionary)
        {
            HashSet<Vector2Int> blastTileCoordinates = new HashSet<Vector2Int>();

            var tasks = new List<Task<HashSet<Vector2Int>>>();
            foreach (Vector2Int coordinate in coordinates)
            {
                tasks.Add(MatchCheck(coordinate, Vector2Int.up, Vector2Int.down, boardDataDictionary));
                tasks.Add(MatchCheck(coordinate, Vector2Int.right, Vector2Int.left, boardDataDictionary));
            }

            var results = await Task.WhenAll(tasks);
            foreach (var result in results)
            {
                blastTileCoordinates.UnionWith(result);
            }

            return blastTileCoordinates;
        }

        async Task<HashSet<Vector2Int>> MatchCheck(Vector2Int coordinate, Vector2Int direction1, Vector2Int direction2, Dictionary<Vector2Int, PlayTile> boardDataDictionary)
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

                if (boardDataDictionary.ContainsKey(current + direction1)
                    && boardDataDictionary[current].TileID == boardDataDictionary[current + direction1].TileID)
                    stack.Push(current + direction1);

                if (boardDataDictionary.ContainsKey(current + direction2)
                    && boardDataDictionary[current].TileID == boardDataDictionary[current + direction2].TileID)
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