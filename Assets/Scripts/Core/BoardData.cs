using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BoardData : MonoBehaviour
    {
        Dictionary<Vector2Int, ContainerTile> containers = new Dictionary<Vector2Int, ContainerTile>();
        public IReadOnlyDictionary<Vector2Int, ContainerTile> Containers { get { return containers; }}

        public void AddContainer(Vector2Int key, ContainerTile tile)
        {
            containers.Add(key, tile);
        }

    }
}

