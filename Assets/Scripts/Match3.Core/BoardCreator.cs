using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class BoardCreator : MonoBehaviour
    {
        [Inject] private InputTileFactory inputTileFactory;
        [SerializeField] private List<InputTileScriptableObject> inputTiles;

        [SerializeField] private int xDim = 8;
        [SerializeField] private int yDim = 10;

        private void Start()
        {
            for(int y=0; y<yDim; y++)
            {
                for(int x=0; x<xDim; x++)
                {
                    InputTile tile = inputTileFactory.Create();
                    tile.SetWorldPosition(new Vector3(x - (xDim - 1) / 2f, y - (yDim - 1) / 2f, 0f));
                    tile.SetBoardPosition(x, y);
                    tile.SetSprite(inputTiles[(x + y) % inputTiles.Count].sprite);
                    tile.transform.SetParent(transform);
                }
            }
        }
    }
}