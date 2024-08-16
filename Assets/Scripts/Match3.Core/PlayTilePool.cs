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

        public PlayTile GetPlayTile()
        {
            if (deactivePlayTilesPool.Count == 0)
                CreateNewPlayTile();

            PlayTile tile = deactivePlayTilesPool.Dequeue();
            tile.Blasted += OnBlasted;
            activePlayTilesPool.Add(tile);
            return tile;   
        }

        PlayTile CreateNewPlayTile()
        {
            PlayTile playTile = playTileFactory.Create();
            PlayTileScriptableObject playTileSO = playTileScriptableObjects[UnityEngine.Random.Range(0, playTileScriptableObjects.Count)];
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

