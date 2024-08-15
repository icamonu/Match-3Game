using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ColumnSorter: MonoBehaviour
    {
        PlayTileFactory playTileFactory;
        List<ContainerTile> listOfChangedContainer = new List<ContainerTile>();
        MatchChecker matchChecker;

        private void Awake()
        {
            playTileFactory = GetComponent<PlayTileFactory>();
            matchChecker = GetComponent<MatchChecker>();
        }

        public void Sort(ContainerTile head)
        {
            listOfChangedContainer.Clear();
            ContainerTile ptrLeft = head;
            ContainerTile ptrRight = head;

            while (ptrLeft != null)
            {
                if (ptrLeft.PlayTile != null)
                {
                    ptrLeft = ptrLeft.neighborTiles[Vector2Int.up];
                    ptrRight = ptrLeft;
                }
                else
                {
                    ptrRight = ptrRight.neighborTiles[Vector2Int.up];

                    if (ptrRight == null)
                    {
                        PlayTile playTile = playTileFactory.CreateARandomTile(new Vector3(head.transform.position.x, 6f, 0f));
                        ptrLeft.SetPlayTile(playTile);
                        listOfChangedContainer.Add(ptrLeft);
                        continue;
                    }

                    if (ptrRight.PlayTile != null)
                    {
                        ptrLeft.SetPlayTile(ptrRight.PlayTile);
                        ptrRight.SetPlayTile(null);
                        ptrLeft = ptrLeft.neighborTiles[Vector2Int.up];
                        listOfChangedContainer.Add(ptrLeft);
                    }
                }
            }
        }
    }
}