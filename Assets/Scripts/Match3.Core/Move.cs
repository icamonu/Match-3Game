using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class Move : MonoBehaviour
    {
        [Inject] InputTile inputTile;
        [Inject] BoardManager boardManager;
        bool isTouching;
        Vector3 startPoint;

        private void OnMouseDown()
        {
            startPoint = UnityEngine.Input.mousePosition;
            isTouching = true;
        }

        private void Update()
        {
            if (!isTouching)
                return;

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                Input();
            }
        }

        private void Input()
        {
            Vector3 direction = UnityEngine.Input.mousePosition - startPoint;
            int directionAxisX = Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? (int)Mathf.Sign(direction.x) : 0;
            int directionAxisY = Mathf.Abs(direction.y) > Mathf.Abs(direction.x) ? (int)Mathf.Sign(direction.y) : 0;

            Vector2Int directionInt = new Vector2Int(directionAxisX, directionAxisY);

            boardManager.FlipMove(inputTile.BoardPosition, inputTile.BoardPosition + directionInt);

            isTouching = false;
        }
    }
}

