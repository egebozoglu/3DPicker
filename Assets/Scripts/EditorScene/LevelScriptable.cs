using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Picker3D.EditorScene
{
    [Serializable]
    public class LevelScriptable : ScriptableObject
    {
        [SerializeField] public int Level;
        [SerializeField] public Color PlatformColor;
        [SerializeField] public List<string> ObjectNames = new List<string>();
        [SerializeField] public List<Vector3> ObjectPositions = new List<Vector3>();
        [SerializeField] public List<Quaternion> ObjectsRotations = new List<Quaternion>();
        [SerializeField] public List<int> CompleteCounts = new List<int>();
    }
}