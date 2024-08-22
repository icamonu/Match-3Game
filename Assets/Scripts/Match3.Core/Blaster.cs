using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Match3.Core
{
    public class Blaster
    {
        public async Task<HashSet<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates, Dictionary<Vector2Int, PlayTile> boardDataDictionary)
        {
            HashSet<Vector2Int> blastedCoordinates=new HashSet<Vector2Int>();

            foreach (Vector2Int i in coordinates)
            {
                boardDataDictionary[i].gameObject.SetActive(false);
                blastedCoordinates.Add(i);
            }

            await Task.Delay(1);

            return blastedCoordinates;
        }
    }
}

