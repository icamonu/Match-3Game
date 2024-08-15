using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Match3.Core
{
    public interface ICommand
    {
        public  Task<IEnumerable<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates);
    }
}

