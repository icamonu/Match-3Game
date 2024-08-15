using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayTileFactory : TileFactoryBase
    {
        [SerializeField] private BoardData boardData;
        [SerializeField] private GameStateController gameStateController;
        [SerializeField] private List<TileScriptableObject> tileScriptableObjects;

        MatchChecker matchChecker;

        private void Awake()
        {
            matchChecker = GetComponent<MatchChecker>();
            gameStateController.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState != GameState.TileCreation)
                return;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Initialize();
            }
        }

        void Initialize()
        {
            foreach(var i in boardData.Containers.Keys)
            {
                do
                {
                    if (boardData.Containers[i].PlayTile != null)
                    {
                        Destroy(boardData.Containers[i].PlayTile.gameObject);
                        boardData.Containers[i].SetPlayTile(null);
                    }
                    CreateARandomTile(i);

                } while (matchChecker.Check(new List<ContainerTile>() { boardData.Containers[i] }, false));
            }
        }

        public PlayTile CreateARandomTile(Vector2Int boardPosition)
        {
            int randomTile = Random.Range(0, tileScriptableObjects.Count);
            PlayTile playTile = GetProduct(boardData.Containers[boardPosition].transform.position, tileScriptableObjects[randomTile], transform) as PlayTile;
            boardData.Containers[boardPosition].SetPlayTile(playTile);

            return playTile;
        }

        public PlayTile CreateARandomTile(Vector3 position)
        {
            int randomTile = Random.Range(0, tileScriptableObjects.Count);
            PlayTile playTile = GetProduct(position, tileScriptableObjects[randomTile], transform) as PlayTile;
            return playTile;
        }
    }
}