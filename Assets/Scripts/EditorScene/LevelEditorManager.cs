using Picker3D.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.EditorScene
{
    public class LevelEditorManager : MonoBehaviour
    {
        #region Variables
        [Header("FPS Data")]
        [SerializeField] private GameManagerData gameManagerData;

        [Header("Prefabs")]
        public List<GameObject> ObjectPrefabs = new List<GameObject>();
        public List<GameObject> PlatformObjectsToColorized;

        [Space]

        [Header("Text Mesh Pros")]
        public TextMesh FirstCompleteText;
        public TextMesh SecondCompleteText;
        public TextMesh ThirdCompleteText;

        // Data for Level Scriptable
        [HideInInspector] public LevelScriptable LoadedLevel;
        [HideInInspector] public Color Color;
        [HideInInspector] public List<GameObject> InstantiatedObjects = new List<GameObject>();

        #endregion

        private void Update()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = gameManagerData.FpsRate;
        }
    }
}