using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Particle
{
    public class ParticleController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Rigidbody rb;
        [SerializeField] private ParticleControllerData particleControllerData;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            rb.AddForce(Vector3.up * particleControllerData.Force);
            Destroy(gameObject, 2f);
        }
    }
}