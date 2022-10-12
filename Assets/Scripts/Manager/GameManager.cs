using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        [Header("Skybox")]
        [SerializeField] private List<Material> skyboxMaterials = new List<Material>();
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            SetSkybox();
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
    }
}
