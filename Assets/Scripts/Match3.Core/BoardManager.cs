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

        public void AddInputTile(InputTile inputTile)
        {
            PlayTile playTile = playTilePool.GetPlayTile(ExludeTiles(inputTile.BoardPosition));
            boardData.SetTile(inputTile.BoardPosition, playTile);

            if (inputTile.BoardPosition == Vector2Int.one)
                boardData.SetOffset();

            playTile.SetPosition(inputTile.transform.position);
        }

        public async void FlipMove(Vector2Int tile1, Vector2Int tile2)
        {
            if (!AreFlippable(tile1, tile2))
                return;

            await Flip(tile1, tile2);
            HashSet<Vector2Int> blastTileCoordinates = await matchChecker.Execute(new HashSet<Vector2Int>() { tile1, tile2 }, boardData.boardDataDictionary);
            if (blastTileCoordinates.Count == 0)
                await Flip(tile1, tile2);
            else
            {
                while (blastTileCoordinates.Count != 0)
                {
                    HashSet<Vector2Int> blastCoordinates = await blaster.Execute(blastTileCoordinates, boardData.boardDataDictionary);

                    foreach(Vector2Int c in blastCoordinates)
                        boardData.SetTile(c, null);

                    HashSet<Vector2Int> sortedCoordinates = await columnSorter.Execute(blastCoordinates);

                    foreach (var i in sortedCoordinates)
                    {
                        if (boardData.boardDataDictionary[i] != null)
                            boardData.boardDataDictionary[i].MoveTo(boardData.boardDataDictionary[i].TargetPosition);
                        else
                        {
                            PlayTile playTile = playTilePool.GetPlayTile();
                            boardData.SetTile(i, playTile);
                            playTile.SetPosition(boardData.GetWorldPosition(i) + new Vector3(0f, 10f, 0f));
                            playTile.MoveTo(boardData.GetWorldPosition(i));
                        }
                    }

                    await Task.Delay(400);
                    blastTileCoordinates = await matchChecker.Execute(sortedCoordinates, boardData.boardDataDictionary);
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

        List<int> ExludeTiles(Vector2Int boardPosition)
        {
            List<int> excludedTiles = new List<int>();

            if (boardPosition.x > 1)
            {
                if (boardData.boardDataDictionary[boardPosition + Vector2Int.left].TileID
                     == boardData.boardDataDictionary[boardPosition + 2 * Vector2Int.left].TileID)
                {
                    excludedTiles.Add(boardData.boardDataDictionary[boardPosition + Vector2Int.left].TileID);
                }
            }

            if (boardPosition.y > 1)
            {
                if (boardData.boardDataDictionary[boardPosition + Vector2Int.down].TileID
                    == boardData.boardDataDictionary[boardPosition + 2 * Vector2Int.down].TileID)
                {
                    excludedTiles.Add(boardData.boardDataDictionary[boardPosition + Vector2Int.down].TileID);
                }
            }

            return excludedTiles;
        }
    }
}