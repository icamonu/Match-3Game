using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class BoardManager : MonoBehaviour
    {
        [Inject] BoardData boardData;
        [Inject] MatchChecker matchChecker;
        [Inject] ColumnSorter columnSorter;
        [Inject] PlayTilePool playTilePool;
        [Inject] Blaster blaster;

        Queue<ICommand> executionQueue = new Queue<ICommand>();

        public void AddInputTile(InputTile inputTile)
        {
            PlayTile playTile = playTilePool.GetPlayTile();
            boardData.SetTile(inputTile.BoardPosition, playTile);
            playTile.SetPosition(inputTile.transform.position);
        }

        public async void FlipMove(Vector2Int tile1, Vector2Int tile2)
        {
            if (!AreFlippable(tile1, tile2))
                return;

            await Flip(tile1, tile2);
            HashSet<Vector2Int> blastTileCoordinates = await matchChecker.Execute(new HashSet<Vector2Int>() { tile1, tile2 });
            if (blastTileCoordinates.Count == 0)
                await Flip(tile1, tile2);
            else
            {
                executionQueue.Enqueue(blaster);
                HashSet<Vector2Int> dummy = await executionQueue.Dequeue().Execute(blastTileCoordinates);

                executionQueue.Enqueue(columnSorter);
                HashSet<Vector2Int> dummy2 = await executionQueue.Dequeue().Execute(dummy);

                foreach(var i in dummy2)
                {
                    Debug.Log(i);
                }
            }
        }

        public async Task Flip(Vector2Int tile1, Vector2Int tile2)
        {
            PlayTile playTile1 = boardData.GetTileData(tile1);
            PlayTile playTile2 = boardData.GetTileData(tile2);
            playTile1.MoveTo(playTile2.transform.position);
            playTile2.MoveTo(playTile1.transform.position);
            await Task.Delay(200);
            boardData.SetTile(tile1, playTile2);
            boardData.SetTile(tile2, playTile1);
            await Task.CompletedTask;
        }

        public bool AreFlippable(Vector2Int tile1, Vector2Int tile2)
        {
            if (boardData.GetTileData(tile1) == null || boardData.GetTileData(tile2) == null)
                return false;

            return true;
        }
    }
}