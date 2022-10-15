using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Picker3D.CollectibleRequired;

namespace Picker3D.Collectible
{
    public class CollectibleObjectController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private AudioSource audioSource;
        private bool collected = false;
        private bool firstCollision = true;
        #endregion

        private void Update()
        {
            if (transform.position.y<=-5)
            {
                Destroy(gameObject, 0f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!firstCollision)
            {
                audioSource.Play();
            }
            
            if (collision.gameObject.tag.Equals("CollectibleRequired") && !collected)
            {
                collected = true;
                var requiredController = collision.transform.GetComponent<CollectibleRequiredController>();
                requiredController.CollectedCount++;
                Destroy(gameObject, 2f);
            }

            firstCollision = false;
        }
    }
}