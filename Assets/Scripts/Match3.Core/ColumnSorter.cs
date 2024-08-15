using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class ColumnSorter: ICommand
    {
        [Inject] BoardData boardData;

        public async Task<HashSet<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates)
        {
            Debug.Log("a");

            HashSet<int> columns = new HashSet<int>();
            HashSet<Vector2Int> emptyBoardCoordinates = new HashSet<Vector2Int>();

            foreach(Vector2Int i in coordinates)
            {
                columns.Add(i.x);
            }

            var tasks = new List<Task<HashSet<Vector2Int>>>();
            foreach (int i in columns)
            {
               tasks.Add(Sort(i));
            }

            Debug.Log("b");
            var results = await Task.WhenAll(tasks);

            foreach (var result in results)
            {
                emptyBoardCoordinates.UnionWith(result);
            }

            return emptyBoardCoordinates;
        }

        //Sorts the given column and returns the set of empty coordinates. Time Complexity: O(n) 
        private async Task<HashSet<Vector2Int>> Sort(int column)
        {
            
            int ptrBack = 0;
            int ptrFrw = 0;

            HashSet<Vector2Int> emptyCoordinates = new HashSet<Vector2Int>();

            await Task.Run(() => {
                while (boardData.BoardDataDictionary.ContainsKey(new Vector2Int(column, ptrBack)))
                {
                    if (boardData.BoardDataDictionary[new Vector2Int(column, ptrBack)] != null)
                    {
                        Debug.Log(ptrBack);
                        ptrBack++;
                        ptrFrw = ptrBack;
                        continue;
                    }

                    if(boardData.BoardDataDictionary.ContainsKey(new Vector2Int(column, ptrFrw)))
                    {
                        if(boardData.BoardDataDictionary[new Vector2Int(column, ptrFrw)] == null)
                        {
                            ptrFrw++;
                            continue;
                        }

                        Vector3 targetPosition = boardData.GetWorldPosition(new Vector2Int(column, ptrBack));
                        boardData.BoardDataDictionary[new Vector2Int(column, ptrFrw)].MoveTo(targetPosition);
                        boardData.SetTile(new Vector2Int(column, ptrBack), boardData.BoardDataDictionary[new Vector2Int(column, ptrFrw)]);
                        boardData.SetTile(new Vector2Int(column, ptrFrw), null);
                    }
                    else
                    {
                        emptyCoordinates.Add(new Vector2Int(column, ptrBack));
                        ptrBack++;
                    }
                }
            });
            return emptyCoordinates;
        }
    }
}