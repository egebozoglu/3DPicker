using Picker3D.Camera;
using System.Collections;
using System.Collections.Generic;
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

        [Header("Game Mechanics")]
        public int TotalCoins = 0;
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
            InstantiatePicker();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetSkybox()
        {
            System.Random rand = new System.Random();
            Material material = skyboxMaterials[rand.Next(skyboxMaterials.Count)];
            RenderSettings.skybox = material;
        }

        private void InstantiatePicker()
        {
            GameObject picker;

            picker = Instantiate(pickerPrefab);
            mainCamera.GetComponent<CameraController>().Picker = picker;
        }
    }
}
