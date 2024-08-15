using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class Blaster : ICommand
    {
        [Inject] BoardData boardData;

        public async Task<IEnumerable<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates)
        {
            foreach (Vector2Int i in coordinates)
                boardData.GetTileData(i).gameObject.SetActive(false);

            await Task.Delay(1);

            return coordinates;
        }
    }
}

