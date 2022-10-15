using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Picker3D.EditorScene
{
    [Serializable]
    public class LevelObject
    {
        [SerializeField] public string ObjectName { get; set; }
        [SerializeField] public Vector3 Position { get; set; }
        [SerializeField] public Quaternion Rotation { get; set; }

    }
}