using Picker3D.CollectibleRequired;
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
        [SerializeField] private List<GameObject> completeRequired;
        #endregion

        public void PlatformSettings()
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
                    renderer.material.color = ItemsColor;
                }
            }
        }

        private void SetCompleteTexts()
        {
            for (int i = 0; i < completeRequired.Count; i++)
            {
                completeRequired[i].GetComponent<CollectibleRequiredController>().RequiredCollectibleCount = CompleteCounts[i];
            }
        }
    }
}