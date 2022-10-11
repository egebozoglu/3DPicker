using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Variables
        public GameObject Picker;
        [SerializeField] private CameraControllerData cameraControllerData;
        #endregion

        private void Awake()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (Picker != null)
            {
                // if picker not null, follow the picker
                Vector3 newPosition = new Vector3(cameraControllerData.Offset.x, cameraControllerData.Offset.y, Picker.transform.position.z + cameraControllerData.Offset.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, cameraControllerData.LerpTime); // smooth movement
            }
        }
    }
}
