using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Picker3D.EditorScene { 

    [CustomEditor(typeof(LevelEditorManager))]
    public class LevelEditor : Editor
    {
        #region Variables
        // Current Level Name
        private string currentLevelName;

        // Complete Counts
        private string firstComplete;
        private string secondComplete;
        private string thirdComplete;
        #endregion

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelEditorManager manager = (LevelEditorManager)target;

            GUILoadLevel();
            GUICreate(manager);
            GUIRemove(manager);
            GUISetCompleteCounts();
        }

        #region GUI Functions
        private void GUILoadLevel()
        {
            GUILine();

            GUILayout.Label("Load Level", EditorStyles.boldLabel);
        }

        private void LoadLevelClick(LevelScriptable levelScriptable, LevelEditorManager manager)
        {
            // destroy existing objects
            foreach (GameObject item in manager.instantiatedObjects)
            {
                DestroyImmediate(item);
            }
            manager.instantiatedObjects.Clear();

            // create selected level objects
            for (int i = 0; i < levelScriptable.allObjects.Count; i++)
            {
                InstantiateObject(levelScriptable.allObjects[i], manager);
            }
            firstComplete = levelScriptable.Complete1.ToString();
            secondComplete = levelScriptable.Complete2.ToString();
            thirdComplete = levelScriptable.Complete3.ToString();
        }

        private void GUICreate(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Create Object", EditorStyles.boldLabel);

            for (int i = 0; i < manager.objectPrefabs.Count; i++)
            {
                if (GUILayout.Button(manager.objectPrefabs[i].name))
                {
                    InstantiateObject(manager.objectPrefabs[i], manager);
                }
            }
        }

        private void GUIRemove(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Remove Objects", EditorStyles.boldLabel);

            for (int i = 0; i < manager.instantiatedObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(manager.instantiatedObjects[i].name);
                if (GUILayout.Button("Remove"))
                {
                    var toRemoveObject = manager.instantiatedObjects[i];
                    manager.instantiatedObjects.Remove(toRemoveObject);
                    DestroyImmediate(toRemoveObject);

                }
                GUILayout.EndHorizontal();
            }
        }

        private void GUISetCompleteCounts()
        {
            GUILine();

            GUILayout.Label("Set Complete Counts", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();

            GUILayout.Label("Phase 1- ");
            firstComplete = EditorGUILayout.TextField(firstComplete);
            GUILayout.Label("Phase 2- ");
            secondComplete = EditorGUILayout.TextField(secondComplete);
            GUILayout.Label("Phase 3- ");
            thirdComplete = EditorGUILayout.TextField(thirdComplete);

            GUILayout.EndHorizontal();
        }

        private void GUILine(int height = 1)
        {
            GUILayout.Space(20);

            Rect rect = EditorGUILayout.GetControlRect(false, height);
            rect.height = height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

            GUILayout.Space(20);
        }
        #endregion

        private void InstantiateObject(GameObject prefab, LevelEditorManager manager)
        {
            GameObject prefabObject;

            prefabObject = Instantiate(prefab);

            manager.instantiatedObjects.Add(prefabObject);
        }
    }
}