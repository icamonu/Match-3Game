using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName ="Scriptable Objects/TileScriptableObject")]
    public class TileScriptableObject : ScriptableObject
    {
        public int tileID;
        public Sprite sprite;

    }
}