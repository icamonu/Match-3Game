using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Core
{
    [RequireComponent(typeof(PlayTile))]
    public class PlayTileMover : MonoBehaviour
    {

        public void Move(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, 0.2f).SetEase(Ease.InQuad);
        }
    }
}