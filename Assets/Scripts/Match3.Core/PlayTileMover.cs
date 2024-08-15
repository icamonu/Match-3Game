using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Match3.Core
{
    public class PlayTileMover : MonoBehaviour
    {

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

