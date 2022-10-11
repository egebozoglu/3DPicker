using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Picker{
    public class PickerController : MonoBehaviour
    {
        #region Variables
        [Header("Movement")]
        private Rigidbody rb;
        [SerializeField] private PickerControllerData pickerControllerData;
        [SerializeField] private bool playable = false;
        #endregion

        private void Awake()
        {
            rb = transform.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (playable)
            {
                rb.MovePosition(rb.position + (Vector3.forward * pickerControllerData.VerticalSpeed * Time.deltaTime));
            }

            int i = 0;

            //loop over every touch found    
            while (i < Input.touchCount)
            {
                if (Input.GetTouch(i).position.x > Screen.width / 2)
                {
                    //move right    
                    HorizontalMove(true);
                }
                if (Input.GetTouch(i).position.x < Screen.width / 2)
                {
                    //move left    
                    HorizontalMove(false);
                }
                ++i;
            }
        }

        private void HorizontalMove(bool right)
        {
            float rightMove = 1;
            if (!right)
            {
                rightMove = -1;
            }
            rb.MovePosition(rb.position + (Vector3.right * rightMove * pickerControllerData.HorizontalSpeed * Time.deltaTime));
        }
    }
}