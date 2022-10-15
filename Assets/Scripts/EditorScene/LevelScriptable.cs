using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.EditorScene
{
    public class LevelScriptable : ScriptableObject
    {
        public int Level;
        public Color PlatformColor;
        public List<LevelObject> AllObjects = new List<LevelObject>();
        public List<int> CompleteCounts = new List<int>();
    }
}