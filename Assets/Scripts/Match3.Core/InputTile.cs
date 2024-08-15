using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class InputTile : MonoBehaviour
    {
        [Inject] BoardManager boardManager;
        [Inject] private SpriteRenderer spriteRenderer;

        public Vector2Int BoardPosition { get; private set; }

        public void SetBoardPosition(int x, int y)
        {
            BoardPosition = new Vector2Int(x, y);
            boardManager.AddInputTile(this);
        }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

    }
}