using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.EditorScene
{
    public class LevelEditorManager : MonoBehaviour
    {
        #region Variables
        [Header("Prefabs")]
        public List<GameObject> objectPrefabs = new List<GameObject>();

        [Space]
        [HideInInspector]public List<GameObject> instantiatedObjects = new List<GameObject>();
        #endregion
    }
}