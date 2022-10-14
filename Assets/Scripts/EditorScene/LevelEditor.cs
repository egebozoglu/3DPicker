using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Picker3D.EditorScene { 

    [CustomEditor(typeof(LevelEditorManager))]
    public class LevelEditor : Editor
    {
        #region Variables
        // Level
        private string currentLevelName;
        private string savingError = string.Empty;
        private List<LevelScriptable> levelScriptables = new List<LevelScriptable>();

        // Complete Counts
        private string firstComplete;
        private string secondComplete;
        private string thirdComplete;

        // Levels Path
        private int levelIndex = 0;
        private string levelPathToSave = "Assets/Resources/Levels/Level";
        #endregion

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelEditorManager manager = (LevelEditorManager)target;

            GUILoadLevel(manager);
            GUIGeneratePlatformColor(manager);
            GUICreate(manager);
            GUIRemove(manager);
            GUISetCompleteCounts();
            GUISaveLevel(manager);
        }

        #region GUI Functions
        private void GUILoadLevel(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Load Level", EditorStyles.boldLabel);

            levelScriptables = new List<LevelScriptable>();
            Object[] leveldata = Resources.LoadAll<LevelScriptable>("Levels");
            foreach (var item in leveldata)
            {
                levelScriptables.Add((LevelScriptable)item);
            }

            List<string> levelNames = new List<string>();
            foreach (var item in levelScriptables)
            {
                levelNames.Add(item.name);
            }

            levelIndex = EditorGUILayout.Popup(levelIndex, levelNames.ToArray());

            if (GUILayout.Button("Load Level"))
            {
                if (levelScriptables.Count!=0)
                {
                    LoadLevelClick(levelScriptables[levelIndex], manager);
                }
            }
        }

        private void LoadLevelClick(LevelScriptable levelScriptable, LevelEditorManager manager)
        {
            // destroy existing objects
            foreach (GameObject item in manager.InstantiatedObjects)
            {
                DestroyImmediate(item);
            }
            manager.InstantiatedObjects.Clear();

            // create selected level objects
            for (int i = 0; i < levelScriptable.AllObjects.Count; i++)
            {
                InstantiateObject(levelScriptable.AllObjects[i], manager);
            }
            firstComplete = levelScriptable.Complete1.ToString();
            secondComplete = levelScriptable.Complete2.ToString();
            thirdComplete = levelScriptable.Complete3.ToString();

            // set platform color
            manager.Color = levelScriptable.PlatformColor;
            foreach (GameObject platformObject in manager.PlatformObjectsToColorized)
            {
                Renderer renderer = platformObject.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.sharedMaterial.color = manager.Color;
                }
            }
            // don't forget to get the level name and index
            manager.LoadedLevel = levelScriptable;
        }

        private void GUIGeneratePlatformColor(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Generate Platform Color", EditorStyles.boldLabel);

            if (GUILayout.Button("Generate Color"))
            {
                manager.Color = Random.ColorHSV();
                foreach (GameObject platformObject in manager.PlatformObjectsToColorized)
                {
                    Renderer renderer = platformObject.GetComponent<Renderer>();
                    
                    if (renderer != null)
                    {
                        renderer.sharedMaterial.color = manager.Color;
                    }
                }
            }
        }

        private void GUICreate(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Create Object", EditorStyles.boldLabel);

            for (int i = 0; i < manager.ObjectPrefabs.Count; i++)
            {
                if (GUILayout.Button(manager.ObjectPrefabs[i].name))
                {
                    InstantiateObject(manager.ObjectPrefabs[i], manager);
                }
            }
        }

        private void GUIRemove(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Remove Objects", EditorStyles.boldLabel);

            for (int i = 0; i < manager.InstantiatedObjects.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(manager.InstantiatedObjects[i].name);
                if (GUILayout.Button("Remove"))
                {
                    var toRemoveObject = manager.InstantiatedObjects[i];
                    manager.InstantiatedObjects.Remove(toRemoveObject);
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

        private void GUISaveLevel(LevelEditorManager manager)
        {
            GUILine();

            GUILayout.Label("Save Level", EditorStyles.boldLabel);

            GUILayout.Space(10);

            GUILayout.Label(savingError, EditorStyles.label);
            if (GUILayout.Button("Save Level"))
            {
                SaveLevelClick(manager);
            }
        }

        private void SaveLevelClick(LevelEditorManager manager)
        {
            if (!string.IsNullOrEmpty(firstComplete) && !string.IsNullOrEmpty(secondComplete) && !string.IsNullOrEmpty(thirdComplete))
            {
                bool firstIsNumeric = int.TryParse(firstComplete, out _);
                bool secondIsNumeric = int.TryParse(secondComplete, out _);
                bool thirdIsNumeric = int.TryParse(thirdComplete, out _);
                if (firstIsNumeric && secondIsNumeric && thirdIsNumeric)
                {
                    // save
                    if (manager.LoadedLevel!=null)
                    {
                        LevelScriptable levelScriptable = manager.LoadedLevel;
                        // set level data
                        SetLevelData(levelScriptable, manager);

                        EditorUtility.SetDirty(levelScriptable);

                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        LevelScriptable levelScriptable = CreateInstance<LevelScriptable>();
                        // set level data
                        levelScriptable.Level = levelScriptables.Count+1;
                        SetLevelData(levelScriptable, manager);

                        string fileName = levelPathToSave + levelScriptable.Level.ToString() +".asset";

                        AssetDatabase.CreateAsset(levelScriptable, fileName);
                        AssetDatabase.SaveAssets();
                    }

                    manager.LoadedLevel = null;
                    manager.Color = Color.blue;
                    foreach (var item in manager.InstantiatedObjects)
                    {
                        DestroyImmediate(item);
                    }
                    manager.InstantiatedObjects.Clear();
                }
                else
                {
                    savingError = "Please Set Complete Counts as Numeric";
                }
            }
            else
            {
                savingError = "Please Set Complete Counts";
            }
        }

        private void SetLevelData(LevelScriptable levelScriptable, LevelEditorManager manager)
        {
            levelScriptable.PlatformColor = manager.Color;
            levelScriptable.AllObjects = manager.InstantiatedObjects;
            levelScriptable.Complete1 = int.Parse(firstComplete);
            levelScriptable.Complete2 = int.Parse(secondComplete);
            levelScriptable.Complete3 = int.Parse(thirdComplete);
        }

        private void GUILine()
        {
            // create line for proper view
            int height = 1;
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

            Vector3 lastPosition;
            if (manager.InstantiatedObjects.Count!=0)
            {
                lastPosition = manager.InstantiatedObjects[manager.InstantiatedObjects.Count - 1].transform.position;
            }
            else
            {
                lastPosition = new Vector3(0, 0.5f, 25);
            }
            
            Vector3 newPosition = lastPosition + new Vector3(0, 0, 5);

            prefabObject = Instantiate(prefab, newPosition, Quaternion.identity);

            manager.InstantiatedObjects.Add(prefabObject);
        }
    }
}