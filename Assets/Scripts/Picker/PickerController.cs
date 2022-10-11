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
        private Vector3 firstTouchPosition;
        private Vector3 secondTouchPosition;
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
                transform.Translate(Vector3.forward * pickerControllerData.VerticalSpeed * Time.deltaTime);
            }

            TouchInputHorizontal();
        }

        private void TouchInputHorizontal()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    firstTouchPosition = Input.GetTouch(0).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    secondTouchPosition = Input.GetTouch(0).position;
                    if (secondTouchPosition.x > firstTouchPosition.x)
                    {
                        HorizontalMove(true);
                    }
                    else
                    {
                        HorizontalMove(false);
                    }
                    firstTouchPosition = secondTouchPosition;
                }
            }
            else
            {
                firstTouchPosition = Vector3.zero;
                secondTouchPosition = Vector3.zero;
            }
        }

        private void HorizontalMove(bool right)
        {
            float rightMove = 1;
            if (!right)
            {
                rightMove = -1;
            }
            transform.Translate(Vector3.right * rightMove * pickerControllerData.HorizontalSpeed * Time.deltaTime);
        }
    }
}