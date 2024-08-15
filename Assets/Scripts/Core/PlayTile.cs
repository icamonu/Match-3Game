using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class PlayTile : MonoBehaviour, ITileProduct
    {
        public int TileID { get; private set; }
        public SpriteRenderer spriteRenderer;
        public Vector3 Position { get; private set;}
        PlayTileMover playTileMover;

        public void Initialize(TileScriptableObject tileScriptableObject)
        {
            TileID = tileScriptableObject.tileID;
            spriteRenderer.sprite = tileScriptableObject.sprite;
            playTileMover = GetComponent<PlayTileMover>();
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            playTileMover.Move(position);
        }
    }
}

