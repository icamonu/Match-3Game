using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class ColumnSorter: ICommand
    {
        [Inject] BoardData boardData;

        public async Task<IEnumerable<Vector2Int>> Execute(IEnumerable<Vector2Int> coordinates)
        {
            HashSet<int> columns = new HashSet<int>();

            foreach(Vector2Int i in coordinates)
            {
                columns.Add(i.x);
            }

            foreach(int i in columns)
            {
                int ptrBack = 0;
                int ptrFrw = 0;

                Vector2Int currentCoordinate;

                while (!boardData.BoardDataDictionary.ContainsKey(new Vector2Int(i, ptrBack)))
                {
                    if (boardData.BoardDataDictionary[new Vector2Int(i, ptrBack)] != null)
                    {
                        ptrBack++;
                        ptrFrw = ptrBack;
                        continue;
                    }

                    if (boardData.BoardDataDictionary[new Vector2Int(i, ptrBack)] == null)
                        ptrBack++;
                    else
                    {
                        if(boardData.BoardDataDictionary.ContainsKey(new Vector2Int(i, ptrFrw)))
                        {
                            Vector3 moveToPosition = boardData.BoardDataDictionary[new Vector2Int(i, ptrBack)].transform.position;
                            boardData.SetTile(new Vector2Int(i, ptrBack), boardData.BoardDataDictionary[new Vector2Int(i, ptrFrw)]);
                            boardData.SetTile(new Vector2Int(i, ptrFrw), null);
                            boardData.BoardDataDictionary[new Vector2Int(i, ptrBack)].MoveTo(moveToPosition);
                        }
                        else
                        {

                        }

                    }
                }
            }


            await Task.Delay(1);
            return coordinates;
        }
    }
}