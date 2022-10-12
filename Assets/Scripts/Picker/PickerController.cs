using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Picker{
    public class PickerController : MonoBehaviour
    {
        #region Variables
        [Header("Movement")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PickerControllerData pickerControllerData;
        [SerializeField] private bool playable = false;
        private Vector3 targetPosition = Vector3.zero;
        private Vector2 firstTouchPosition;
        private Vector2 secondTouchPosition;
        #endregion

        private void Awake()
        {
            Application.targetFrameRate = 30;
        }

        // Update is called once per frame
        private void Update()
        {
            TouchInputHorizontal();
            Movement();
        }

        private void Movement()
        {
            if (targetPosition != Vector3.zero)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, pickerControllerData.VerticalSpeed * Time.deltaTime));
            }

            // Check Border
            if (transform.position.x < pickerControllerData.MinHorizontal)
            {
                transform.position = new Vector3(pickerControllerData.MinHorizontal + 0.02f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > pickerControllerData.MaxHorizontal)
            {
                transform.position = new Vector3(pickerControllerData.MaxHorizontal - 0.02f, transform.position.y, transform.position.z);
            }
        }

        private void TouchInputHorizontal()
        {
            if (Input.touchCount > 0)
            {
                playable = true;
                //if (Input.GetTouch(0).phase == TouchPhase.Began)
                //{
                //    firstTouchPosition = Input.GetTouch(0).position;
                //}

                //if (Input.GetTouch(0).phase == TouchPhase.Moved)
                //{
                //    secondTouchPosition = Input.GetTouch(0).position;
                //    if (secondTouchPosition.x > firstTouchPosition.x)
                //    {
                //        HorizontalMove(true);
                //    }
                //    else
                //    {
                //        HorizontalMove(false);
                //    }
                //    firstTouchPosition = secondTouchPosition;
                //}

                secondTouchPosition = Input.GetTouch(0).position;
                if (secondTouchPosition.x > firstTouchPosition.x)
                {
                    HorizontalMove(true);
                }
                else if(secondTouchPosition.x < firstTouchPosition.x)
                {
                    HorizontalMove(false);
                }
                else
                {
                    targetPosition = transform.position + Vector3.forward;
                }
                firstTouchPosition = secondTouchPosition;
            }
            else
            {
                firstTouchPosition = Vector2.zero;
                secondTouchPosition = Vector2.zero;

                if (playable)
                {
                    targetPosition = transform.position + Vector3.forward;
                }
            }
        }

        private void HorizontalMove(bool right)
        {
            float rightMove = 1;
            if (!right)
            {
                rightMove = -1;
            }

            if (playable)
            {
                targetPosition = transform.position + Vector3.forward + (Vector3.right * rightMove);
            }
            else
            {
                targetPosition = transform.position + (Vector3.right * rightMove);
            }
        }
    }
}