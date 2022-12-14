using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Picker3D.EditorScene { 

    [CustomEditor(typeof(LevelEditorManager))]
    public class LevelEditor : Editor
    {
        #region Variables
        LevelEditorManager manager;

        // Random Generate Level
        private string randomFirstPhase = "0";
        private string randomSecondPhase = "0";
        private string randomThirdPhase = "0";
        private float randomPositionX; // between -8 and 8
        private float randomPositionY = 0.5f;
        private float randomPositionZ; // depends on phase => firstPhase between 25 and 75 , secondPhase between 130 and 215 , thirdPhase between 270 and 355

        // Level
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

            manager = (LevelEditorManager)target;

            GUIGenerateRandomLevel();
            GUINewLevel();
            GUILoadLevel();
            GUIGeneratePlatformColor();
            GUICreate();
            GUIRemove();
            GUISetCompleteCounts();
            GUISaveLevel();
        }

        #region GUI Functions

        private void GUIGenerateRandomLevel()
        {
            GUILine();

            GUILayout.Label("Generate Random Level", EditorStyles.boldLabel);

            GUILayout.Space(10);
            GUILayout.Label("Object counts for each phase", EditorStyles.label);

            GUILayout.BeginHorizontal();

            GUILayout.Label("Phase 1- ");
            randomFirstPhase = EditorGUILayout.TextField(randomFirstPhase);
            GUILayout.Label("Phase 2- ");
            randomSecondPhase = EditorGUILayout.TextField(randomSecondPhase);
            GUILayout.Label("Phase 3- ");
            randomThirdPhase = EditorGUILayout.TextField(randomThirdPhase);

            GUILayout.EndHorizontal();


            if (GUILayout.Button("Generate Level"))
            {
                // clear the existing level
                NewLevel();
                // generate objects randomly
                GeneratingRandomObjects();
            }
        }

        private void GeneratingRandomObjects()
        {
            System.Random rand = new System.Random();
            InstantiateRandomObjects(randomFirstPhase, 25, 75, manager.ObjectPrefabs[rand.Next(manager.ObjectPrefabs.Count)]);
            InstantiateRandomObjects(randomSecondPhase, 130, 215, manager.ObjectPrefabs[rand.Next(manager.ObjectPrefabs.Count)]);
            InstantiateRandomObjects(randomThirdPhase, 270, 355, manager.ObjectPrefabs[rand.Next(manager.ObjectPrefabs.Count)]);
        }

        private void InstantiateRandomObjects(string randomPhase, int minRandomZ, int maxRandomZ, GameObject randomPrefab)
        {
            System.Random rand = new System.Random();
            if (CheckRandomPhaseCounts(randomPhase))
            {
                for (int i = 0; i < int.Parse(randomPhase); i++)
                {
                    
                    randomPositionX = rand.Next(-8, 9);
                    if (randomPositionX % 2 != 0)
                    {
                        randomPositionX += 2 - (randomPositionX % 2);
                        randomPositionX *= rand.Next(-1, 2);
                    }
                    var step = (i * 5) % (maxRandomZ - minRandomZ);
                    var randomZ = minRandomZ + step;
                    Vector3 position = new Vector3(randomPositionX, randomPositionY, randomZ);

                    GameObject randomObject;
                    randomObject = Instantiate(randomPrefab, position, Quaternion.identity);
                    manager.InstantiatedObjects.Add(randomObject);
                }
            }
        }

        private bool CheckRandomPhaseCounts(string randomPhase)
        {
            bool isNumeric = int.TryParse(randomPhase, out _);
            if (randomPhase != "0" && isNumeric)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GUINewLevel()
        {
            GUILine();

            GUILayout.Label("New Level", EditorStyles.boldLabel);

            if (GUILayout.Button("New Blank Level"))
            {
                NewLevel();
                manager.LoadedLevel = null;
            }
        }

        private void NewLevel()
        {
            // destroy existing objects
            foreach (GameObject item in manager.InstantiatedObjects)
            {
                DestroyImmediate(item);
            }
            manager.InstantiatedObjects.Clear();
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
            for (int i = 0; i < levelScriptable.ObjectNames.Count; i++)
            {
                GameObject instantiatedObject= InstantiateObject(levelScriptable.ObjectNames[i]);
                instantiatedObject.transform.position = levelScriptable.ObjectPositions[i];
                instantiatedObject.transform.rotation = levelScriptable.ObjectsRotations[i];
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

            GUILayout.Label("Complete 1- ");
            firstComplete = EditorGUILayout.TextField(firstComplete);
            GUILayout.Label("Complete 2- ");
            secondComplete = EditorGUILayout.TextField(secondComplete);
            GUILayout.Label("Complete 3- ");
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

                    savingError = string.Empty;
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
            CreateLevelObjectList(levelScriptable);
            List<int> completeList = new List<int>();
            completeList.Add(int.Parse(firstComplete));
            completeList.Add(int.Parse(secondComplete));
            completeList.Add(int.Parse(thirdComplete));
            levelScriptable.CompleteCounts = completeList;
        }

        private void CreateLevelObjectList(LevelScriptable levelScriptable)
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

            levelScriptable.ObjectNames.Clear();
            levelScriptable.ObjectPositions.Clear();
            levelScriptable.ObjectsRotations.Clear();

            foreach (LevelObject levelObject in levelObjects)
            {
                levelScriptable.ObjectNames.Add(levelObject.ObjectName);
                levelScriptable.ObjectPositions.Add(levelObject.Position);
                levelScriptable.ObjectsRotations.Add(levelObject.Rotation);
            }
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