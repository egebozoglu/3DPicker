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
        [SerializeField] private Rigidbody rb;
        private bool collected = false;
        private bool firstCollision = true;
        private string triggerTag = "InsideTrigger";
        private float insideDrag = 5;
        private float outsideDrag = 0.5f;
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

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag.Equals(triggerTag))
            {
                rb.drag = insideDrag;
                Debug.Log(rb.drag.ToString());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals(triggerTag))
            {
                rb.drag = outsideDrag;
            }
        }
    }
}