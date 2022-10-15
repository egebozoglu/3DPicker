using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Picker3D.EditorScene { 

    [CustomEditor(typeof(LevelEditorManager))]
    public class LevelEditor : Editor
    {
        #region Variables
        LevelEditorManager manager;

        // Level
        private string currentLevelName;
        private string savingError = string.Empty;
        private List<LevelScriptable> levelScriptables = new List<LevelScriptable>();

        // Complete Counts
        private string firstComplete = "0";
        private string secondComplete = "0";
        private string thirdComplete = "0";

        // Levels Path
        private int levelIndex = 0;
        private string levelPathToSave = "Assets/Resources/Levels/Level";
        #endregion

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            manager = (LevelEditorManager)target;

            GUINewLevel();
            GUILoadLevel();
            GUIGeneratePlatformColor();
            GUICreate();
            GUIRemove();
            GUISetCompleteCounts();
            GUISaveLevel();
        }

        #region GUI Functions

        private void GUINewLevel()
        {
            GUILine();

            GUILayout.Label("New Level", EditorStyles.boldLabel);

            if (GUILayout.Button("New Level"))
            {
                // destroy existing objects
                foreach (GameObject item in manager.InstantiatedObjects)
                {
                    DestroyImmediate(item);
                }
                manager.InstantiatedObjects.Clear();
                manager.LoadedLevel = null;
                manager.Color = Random.ColorHSV();
            }
        }
        private void GUILoadLevel()
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
                    LoadLevelClick(levelScriptables[levelIndex]);
                }
            }
        }

        private void LoadLevelClick(LevelScriptable levelScriptable)
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
                var levelObject = levelScriptable.AllObjects[i];
                GameObject instantiatedObject= InstantiateObject(levelObject.ObjectName);
                instantiatedObject.transform.position = levelObject.Position;
                instantiatedObject.transform.rotation = levelObject.Rotation;
            }
            firstComplete = levelScriptable.CompleteCounts[0].ToString();
            secondComplete = levelScriptable.CompleteCounts[1].ToString();
            thirdComplete = levelScriptable.CompleteCounts[2].ToString();

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

        private void GUIGeneratePlatformColor()
        {
            GUILine();

            GUILayout.Label("Generate Platform Color", EditorStyles.boldLabel);

            if (GUILayout.Button("Generate Color"))
            {
                manager.Color = Random.ColorHSV();
            }

            SetPlatformColor();
        }

        private void SetPlatformColor()
        {
            foreach (GameObject platformObject in manager.PlatformObjectsToColorized)
            {
                Renderer renderer = platformObject.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.sharedMaterial.color = manager.Color;
                }
            }
        }

        private void GUICreate()
        {
            GUILine();

            GUILayout.Label("Create Object", EditorStyles.boldLabel);

            for (int i = 0; i < manager.ObjectPrefabs.Count; i++)
            {
                if (GUILayout.Button(manager.ObjectPrefabs[i].name))
                {
                    InstantiateObject(manager.ObjectPrefabs[i].name);
                }
            }
        }

        private void GUIRemove()
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

            manager.FirstCompleteText.text = "0/" + firstComplete;
            manager.SecondCompleteText.text = "0/" + secondComplete;
            manager.ThirdCompleteText.text = "0/" + thirdComplete;

            GUILayout.EndHorizontal();
        }

        private void GUISaveLevel()
        {
            GUILine();

            GUILayout.Label("Save Level", EditorStyles.boldLabel);

            GUILayout.Space(10);

            GUILayout.Label(savingError, EditorStyles.label);
            if (GUILayout.Button("Save Level"))
            {
                SaveLevelClick();
            }
        }

        private void SaveLevelClick()
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
                        SetLevelData(levelScriptable);

                        EditorUtility.SetDirty(levelScriptable);
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        LevelScriptable levelScriptable = CreateInstance<LevelScriptable>();
                        // set level data
                        levelScriptable.Level = levelScriptables.Count+1;
                        SetLevelData(levelScriptable);

                        string fileName = levelPathToSave + levelScriptable.Level.ToString() +".asset";

                        AssetDatabase.CreateAsset(levelScriptable, fileName);
                        EditorUtility.SetDirty(levelScriptable);
                        AssetDatabase.SaveAssets();
                    }

                    manager.LoadedLevel = null;
                    manager.Color = Random.ColorHSV();
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

        private void SetLevelData(LevelScriptable levelScriptable)
        {
            levelScriptable.PlatformColor = manager.Color;
            levelScriptable.AllObjects = CreateLevelObjectList();
            List<int> completeList = new List<int>();
            completeList.Add(int.Parse(firstComplete));
            completeList.Add(int.Parse(secondComplete));
            completeList.Add(int.Parse(thirdComplete));
            levelScriptable.CompleteCounts = completeList;
        }

        private List<LevelObject> CreateLevelObjectList()
        {
            List<LevelObject> levelObjects = new List<LevelObject>();

            foreach (GameObject instantiatedObject in manager.InstantiatedObjects)
            {
                LevelObject levelObject = new LevelObject();
                for (int i = 0; i < manager.ObjectPrefabs.Count; i++)
                {
                    if (instantiatedObject.name.Contains(manager.ObjectPrefabs[i].name))
                    {
                        levelObject.ObjectName = manager.ObjectPrefabs[i].name;
                    }
                }
                levelObject.Position = instantiatedObject.transform.position;
                levelObject.Rotation = instantiatedObject.transform.rotation;
                levelObjects.Add(levelObject);
            }

            return levelObjects;
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

        private GameObject InstantiateObject(string prefabName)
        {
            GameObject prefab = manager.ObjectPrefabs[0];
            for (int i = 0; i < manager.ObjectPrefabs.Count; i++)
            {
                if (manager.ObjectPrefabs[i].name.Contains(prefabName))
                {
                    prefab = manager.ObjectPrefabs[i];
                }
            }
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
            return prefabObject;
        }
    }
}