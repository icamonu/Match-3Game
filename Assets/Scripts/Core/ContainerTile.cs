using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class ContainerTile : MonoBehaviour, ITileProduct
    {
        [SerializeField] public SpriteRenderer spriteRenderer;
        public int TileID { get; private set; }
        public Vector2Int BoardPosition { get; private set; }
        public PlayTile PlayTile { get; private set; }
        public Dictionary<Vector2Int, ContainerTile> neighborTiles = new Dictionary<Vector2Int, ContainerTile>();
        public MatchChecker MatchChecker { get; set; }

        public void Initialize(TileScriptableObject tileScriptableObject)
        {
            TileID = tileScriptableObject.tileID;
            spriteRenderer.sprite = tileScriptableObject.sprite;
        }

        public void SetBoardPosition(Vector2Int boardPosition)
        {
            BoardPosition = boardPosition;
        }

        public async void SetPlayTile(PlayTile playTile)
        {
            PlayTile = playTile;

            if (playTile != null)
            {
                PlayTile.SetPosition(transform.position);
                await Task.Delay(400);
                MatchChecker.Check(new List<ContainerTile>() { this }, true);
            }
        }
    }
}