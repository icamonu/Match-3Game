using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Match3.Core
{
    public interface ICommand
    {
        public  Task<HashSet<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates, Dictionary<Vector2Int, PlayTile> boardDataDictionary);
    }
}