using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.EditorScene
{
    public class LevelScriptable : ScriptableObject
    {
        public int Level;
        public Color PlatformColor;
        public List<GameObject> AllObjects = new List<GameObject>();
        public int Complete1;
        public int Complete2;
        public int Complete3;
    }
}