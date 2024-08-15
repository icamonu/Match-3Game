using UnityEngine;

namespace Core
{
    public abstract class TileFactoryBase : MonoBehaviour
    {
        [SerializeField] protected GameObject tilePrefab;

        public virtual ITileProduct GetProduct(Vector3 position, TileScriptableObject tileScriptableObject, Transform parent = null)
        {
            ITileProduct tileProduct = Instantiate(tilePrefab, position, Quaternion.identity, parent).GetComponent<ITileProduct>();
            tileProduct.Initialize(tileScriptableObject);
            return tileProduct;          
        }
    }
}