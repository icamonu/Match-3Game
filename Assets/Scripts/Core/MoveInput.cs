using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(ContainerTile))]
    public class MoveInput : MonoBehaviour
    {
        bool isTouching = false;
        Vector3 startPoint;
        private ContainerTile containerTile;
        public MatchChecker MatchChecker { get; set; }



        private void Start()
        {
            containerTile = GetComponent<ContainerTile>();
        }

        private void OnMouseDown()
        {
            startPoint = UnityEngine.Input.mousePosition;
            isTouching=true;
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

            Vector2Int dir = new Vector2Int(directionAxisX, directionAxisY);

            PlayTileFlipper.Flip(containerTile, containerTile.neighborTiles[dir]);
            bool isMatch = MatchChecker.Check(new List<ContainerTile>() { containerTile, containerTile.neighborTiles[dir] }, false);
            if (!isMatch)
                PlayTileFlipper.Flip(containerTile.neighborTiles[dir], containerTile);

            isTouching = false;
        }
    }
}