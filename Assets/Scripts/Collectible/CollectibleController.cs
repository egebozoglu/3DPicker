using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Collectible
{
    public class CollectibleController : MonoBehaviour
    {
        #region Variables
        public CollectibleControllerData CollectibleControllerData;
        #endregion

        void Start()
        {
            InstantiateObject();
        }

        private void InstantiateObject()
        {
            Instantiate(CollectibleControllerData.CollectibleObject, gameObject.transform);
        }
    }
}