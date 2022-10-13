using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Picker3D.CollectibleRequired;

namespace Picker3D.Collectible
{
    public class CollectibleObjectController : MonoBehaviour
    {
        #region Variables
        private bool collected = false;
        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("CollectibleRequired") && !collected)
            {
                collected = true;
                var requiredController = collision.transform.GetComponent<CollectibleRequiredController>();
                requiredController.CollectedCount++;
                requiredController.CollectingStarted = true;
                Destroy(gameObject, 2f);
            }
        }
    }
}