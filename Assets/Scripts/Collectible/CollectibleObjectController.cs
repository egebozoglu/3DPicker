using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Picker3D.CollectibleRequired;
using Picker3D.Manager;

namespace Picker3D.Collectible
{
    public class CollectibleObjectController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject particlePrefab;
        private int particleCount = 3;
        private bool collected = false;
        private bool firstCollision = true;
        private string triggerTag = "InsideTrigger";
        private float insideDrag = 10;
        private float outsideDrag = 0.5f;
        private float mass = 5000;
        #endregion

        private void Update()
        {
            if (transform.position.y <= -5 || GameManager.Instance.levelEnd)
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
                StartCoroutine(InstantiateParticle());
                collected = true;
                var requiredController = collision.transform.GetComponent<CollectibleRequiredController>();
                requiredController.CollectedCount++;
                Destroy(gameObject, 2f);
            }

            firstCollision = false;
        }

        private IEnumerator InstantiateParticle()
        {
            yield return new WaitForSeconds(1.8f);

            for (int i = 0; i < particleCount; i++)
            {
                GameObject particle;
                particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

                Renderer renderer = particle.GetComponent<Renderer>();
                if (renderer!=null)
                {
                    renderer.material.color = Random.ColorHSV();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag.Equals(triggerTag))
            {
                rb.drag = insideDrag;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals(triggerTag))
            {
                rb.drag = outsideDrag;
                rb.mass = mass;
            }
        }
    }
}