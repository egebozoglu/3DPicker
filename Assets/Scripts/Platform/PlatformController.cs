using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Platform
{
    public class PlatformController : MonoBehaviour
    {
        #region Variables
        [HideInInspector] public Color ItemsColor;
        [Header("Platform Items")]
        [SerializeField] private List<GameObject> itemsToColorize;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            ColorizeItems();
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
    }
}