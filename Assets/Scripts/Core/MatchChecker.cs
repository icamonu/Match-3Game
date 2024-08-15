using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class MatchChecker: MonoBehaviour
    {
        ColumnSorter columnSorter;

        private void Awake()
        {
            columnSorter = GetComponent<ColumnSorter>();
        }

        public HashSet<ContainerTile> AxisCheck(ContainerTile containerTile, Vector2Int axis)
        {

            Stack<ContainerTile> nodeStack = new Stack<ContainerTile>();
            HashSet<ContainerTile> visitedNodes = new HashSet<ContainerTile>();
            HashSet<ContainerTile> blastTiles = new HashSet<ContainerTile>();
            nodeStack.Push(containerTile);

            while (nodeStack.Count != 0)
            {
                ContainerTile current = nodeStack.Pop();

                if (current.PlayTile == null)
                    continue;

                if (current.PlayTile.TileID == containerTile.PlayTile.TileID)
                {
                    blastTiles.Add(current);

                    if (!visitedNodes.Contains(current.neighborTiles[axis]) && current.neighborTiles[axis] != null)
                        nodeStack.Push(current.neighborTiles[axis]);

                    if (!visitedNodes.Contains(current.neighborTiles[-1*axis]) && current.neighborTiles[-1*axis] != null)
                        nodeStack.Push(current.neighborTiles[-1*axis]);
                }

                visitedNodes.Add(current);
            }

            if (blastTiles.Count < 3)
                return new HashSet<ContainerTile>() { };

            return blastTiles;
        }

        //This method combines vertical and horizontal checks and returns a bool value if there is a match
        public bool Check(IEnumerable<ContainerTile> containerTiles, bool blast=true)
        {
            HashSet<ContainerTile> blastTiles = new HashSet<ContainerTile>();

            foreach(ContainerTile containerTile in containerTiles)
            {
                blastTiles.UnionWith(AxisCheck(containerTile, Vector2Int.right));
                blastTiles.UnionWith(AxisCheck(containerTile, Vector2Int.up));
            }

            if (blastTiles.Count == 0)
                return false;

            if (!blast)
                return true;

            Dictionary<int, ContainerTile> heads = new Dictionary<int, ContainerTile>();

            foreach (ContainerTile tile in blastTiles)
            {
                if (tile.PlayTile == null)
                    continue;

                tile.PlayTile.gameObject.SetActive(false);
                tile.SetPlayTile(null);

                if (!heads.ContainsKey(tile.BoardPosition.x))
                {
                    heads.Add(tile.BoardPosition.x, tile);
                }
                else
                {
                    if (heads[tile.BoardPosition.x].BoardPosition.y > tile.BoardPosition.y)
                        heads[tile.BoardPosition.x] = tile;
                }
            }

            foreach (var i in heads)
            {
                columnSorter.Sort(i.Value);
            }

            return true;
        }
    }
}