using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Match3.Core
{
    public class PlayTile : MonoBehaviour
    {
        public Action<PlayTile> Blasted;
        public int TileID { get; private set; }
        
        [Inject]private SpriteRenderer spriteRenderer;

        public void Initialize(Sprite sprite, int tileID)
        {
            spriteRenderer.sprite = sprite;
            TileID = tileID;
        }

        private void OnDisable()
        {
            Blasted?.Invoke(this);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void MoveTo(Vector3 position)
        {
            transform.DOMove(position, 0.2f).SetEase(Ease.InCubic);
        }
    }

}
