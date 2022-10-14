using Picker3D.CollectibleRequired;
using Picker3D.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Picker{
    public class PickerController : MonoBehaviour
    {
        #region Variables
        public static PickerController Instance;

        [Header("Movement")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PickerControllerData pickerControllerData;
        public bool Playable = false;
        private bool initialPlayable = true;
        private Vector3 targetPosition = Vector3.zero;
        private Vector2 firstTouchPosition;
        private Vector2 secondTouchPosition;
        #endregion

        private void Awake()
        {
            Application.targetFrameRate = 30;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
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
                if (initialPlayable)
                {
                    UIManager.Instance.GameScreen();
                    Playable = true;
                    initialPlayable = false;
                }

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
                    if (Playable)
                    {
                        targetPosition = transform.position + Vector3.forward;
                    }
                    else
                    {
                        targetPosition = Vector3.zero;
                    }
                }
                firstTouchPosition = secondTouchPosition;
            }
            else
            {
                firstTouchPosition = Vector2.zero;
                secondTouchPosition = Vector2.zero;

                if (Playable)
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

            if (Playable)
            {
                targetPosition = transform.position + Vector3.forward + (Vector3.right * rightMove);
            }
            else
            {
                targetPosition = transform.position + (Vector3.right * rightMove);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("EndSection"))
            {
                Playable = false;
                Destroy(other.gameObject,0f);
                
            }
            else if (other.gameObject.tag.Equals("LevelFinish"))
            {
                Playable = false;
                UIManager.Instance.EndScreen(0, GameManager.Instance.TotalCoins);
                var level = PlayerPrefs.GetInt("Level");
                PlayerPrefs.SetInt("Level", level + 1);
                Destroy(other.gameObject, 0f);
            }
        }
    }
}