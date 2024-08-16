using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class PlayTilePool : MonoBehaviour
    {
        [Inject] private PlayTileFactory playTileFactory;

        HashSet<PlayTile> activePlayTilesPool = new HashSet<PlayTile>();
        Queue<PlayTile> deactivePlayTilesPool = new Queue<PlayTile>();
        [SerializeField] private List<PlayTileScriptableObject> playTileScriptableObjects;

        public PlayTile GetPlayTile(List<int> exludedTiles)
        {
            if (deactivePlayTilesPool.Count == 0)
                CreateNewPlayTile(exludedTiles);

            PlayTile tile = deactivePlayTilesPool.Dequeue();
            tile.Blasted += OnBlasted;
            activePlayTilesPool.Add(tile);
            return tile;   
        }

        public PlayTile GetPlayTile()
        {
            if (deactivePlayTilesPool.Count == 0)
                CreateNewPlayTile(new List<int>());

            PlayTile tile = deactivePlayTilesPool.Dequeue();
            tile.Blasted += OnBlasted;
            activePlayTilesPool.Add(tile);
            return tile;
        }

        PlayTile CreateNewPlayTile(List<int> exludedTiles)
        {
            PlayTile playTile = playTileFactory.Create();

            int rndNumber= Random.Range(0, playTileScriptableObjects.Count);

            if (exludedTiles.Count != 0)
            {
                Debug.Log(exludedTiles[0]);
            }

            while (exludedTiles.Contains(rndNumber))
            {
                rndNumber = Random.Range(0, playTileScriptableObjects.Count);
            }

            PlayTileScriptableObject playTileSO = playTileScriptableObjects[rndNumber];
            playTile.Initialize(playTileSO.sprite, playTileSO.tileID);
            deactivePlayTilesPool.Enqueue(playTile);
            playTile.transform.SetParent(transform);
            return playTile;
        }

        private void OnBlasted(PlayTile playTile)
        {
            activePlayTilesPool.Remove(playTile);
            playTile.Blasted -= OnBlasted;
        }
    }
}

