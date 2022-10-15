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
        [SerializeField] public List<LevelObject> AllObjects = new List<LevelObject>();
        [SerializeField] public List<int> CompleteCounts = new List<int>();
    }
}