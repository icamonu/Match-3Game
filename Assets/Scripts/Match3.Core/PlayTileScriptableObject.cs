using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Core
{
    [CreateAssetMenu(menuName = "Scriptable Objects/PlayTileScriptableObject")]
    public class PlayTileScriptableObject : ScriptableObject
    {

        public Sprite sprite;
        public int tileID;

    }
}