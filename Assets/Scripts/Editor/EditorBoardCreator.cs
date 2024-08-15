using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Editor
{
    public class EditorBoardCreator : TileFactoryBase
    {
        [SerializeField] private int boardSizeX = 9;
        [SerializeField] private int boardSizeY = 10;
        [SerializeField] ContainerTileFactory containerTileFactory;
        [SerializeField] List<TileScriptableObject> tileScriptableObjects;

        private void Awake()
        {
            for(int y=0; y<boardSizeY; y++)
            {
                for(int x=0; x<boardSizeX; x++)
                {
                    GetProduct(new Vector3(x-(boardSizeX-1f)/2f, y-(boardSizeY-1f)/2f, 0), tileScriptableObjects[(x + y) % 2], transform);
                }
            }
        }
    }
}