using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Platform
{
    public class PlatformController : MonoBehaviour
    {
        #region Variables
        [HideInInspector] public Color ItemsColor;
        [HideInInspector] public List<int> CompleteCounts;
        [Header("Platform Items")]
        [SerializeField] private List<GameObject> itemsToColorize;
        [SerializeField] private List<TextMesh> completeTextMeshes;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            ColorizeItems();
            SetCompleteTexts();
        }

        private void ColorizeItems()
        {
            foreach (GameObject item in itemsToColorize)
            {
                Renderer renderer = item.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.sharedMaterial.color = ItemsColor;
                }
            }
        }

        private void SetCompleteTexts()
        {
            for (int i = 0; i < completeTextMeshes.Count; i++)
            {
                completeTextMeshes[i].text = "0/" + CompleteCounts[i].ToString();
            }
        }
    }
}