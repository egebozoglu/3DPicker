using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.EditorScene
{
    public class LevelEditorManager : MonoBehaviour
    {
        #region Variables
        [Header("Prefabs")]
        public List<GameObject> ObjectPrefabs = new List<GameObject>();
        public List<GameObject> PlatformObjectsToColorized;

        [Space]
        [Header("Data for Level Scriptable")]
        [HideInInspector] public LevelScriptable LoadedLevel;
        [HideInInspector] public Color Color;
        [HideInInspector] public List<GameObject> InstantiatedObjects = new List<GameObject>();
        #endregion

        private void Update()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }
    }
}