using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class NavigationData : ScriptableObject
    {
        public int width;

        public int length;

        public float nodeSize;

        public List<Vector2Int> lockNodes = new List<Vector2Int>();
    }
}
