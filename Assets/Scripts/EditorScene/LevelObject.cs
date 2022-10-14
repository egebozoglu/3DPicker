using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.EditorScene
{
    public class LevelObject
    {
        public GameObject ObjectPrefab { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

    }
}