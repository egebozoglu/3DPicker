using Picker3D.Camera;
using Picker3D.EditorScene;
using Picker3D.Platform;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Picker3D.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager Instance;

        [Header("Skybox")]
        [SerializeField] private List<Material> skyboxMaterials = new List<Material>();

        [Header("Picker")]
        [SerializeField] private GameObject pickerPrefab;
        [SerializeField] private GameObject mainCamera;

        [Header("Platform")]
        [SerializeField] private GameObject platformBasePrefab;

        [Header("Game Mechanics")]
        [HideInInspector] public int TotalCoins = 0;
        [SerializeField] private GameManagerData gameManagerData;
        [HideInInspector] public bool levelEnd = false;

        [Header("Level")]
        private LevelScriptable levelScriptable;
        [SerializeField] private List<GameObject> collectibleObjectPrefabs = new List<GameObject>();
        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            var level = PlayerPrefs.GetInt("Level");
            if (level==0)
            {
                PlayerPrefs.SetInt("Level", 1);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SetSkybox();
            LoadLevel();
            InstantiatePlatform();
            InstantiatePicker();
            InstantiateCollectibles();
        }

        // Update is called once per frame
        void Update()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = gameManagerData.FpsRate;
        }

        private void SetSkybox()
        {
            System.Random rand = new System.Random();
            Material material = skyboxMaterials[rand.Next(skyboxMaterials.Count)];
            RenderSettings.skybox = material;
        }

        private void LoadLevel()
        {
            int level = PlayerPrefs.GetInt("Level");
            List<LevelScriptable> levelScriptables = new List<LevelScriptable>();
            Object[] leveldata = Resources.LoadAll<LevelScriptable>("Levels");
            foreach (var item in leveldata)
            {
                levelScriptables.Add((LevelScriptable)item);
            }

            if (level > levelScriptables.Count)
            {
                System.Random rand = new System.Random();
                levelScriptable = levelScriptables[rand.Next(0,levelScriptables.Count)];
            }
            else
            {
                levelScriptable = levelScriptables.Where(x => x.name == "Level" + level.ToString()).FirstOrDefault();
            }
        }

        private void InstantiatePlatform()
        {
            GameObject platform;

            platform = Instantiate(platformBasePrefab);
            var platformController = platform.GetComponent<PlatformController>();
            platformController.ItemsColor = levelScriptable.PlatformColor;
            platformController.CompleteCounts = levelScriptable.CompleteCounts;
            platformController.PlatformSettings();
        }

        private void InstantiatePicker()
        {
            GameObject picker;

            picker = Instantiate(pickerPrefab);
            mainCamera.GetComponent<CameraController>().Picker = picker;
        }

        private void InstantiateCollectibles()
        {
            for (int i = 0; i < levelScriptable.ObjectNames.Count; i++)
            {
                GameObject prefab = collectibleObjectPrefabs[0];

                for (int j = 0; j < collectibleObjectPrefabs.Count; j++)
                {
                    if (levelScriptable.ObjectNames[i] == collectibleObjectPrefabs[j].name)
                    {
                        prefab = collectibleObjectPrefabs[j];
                    }
                }

                Instantiate(prefab, levelScriptable.ObjectPositions[i], levelScriptable.ObjectsRotations[i]);
            }
        }
    }
}
