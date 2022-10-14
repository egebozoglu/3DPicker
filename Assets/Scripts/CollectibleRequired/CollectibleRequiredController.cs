using Picker3D.Manager;
using Picker3D.Picker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.CollectibleRequired
{
    public class CollectibleRequiredController : MonoBehaviour
    {
        #region Variables
        public int RequiredCollectibleCount;
        public int CollectedCount;
        private bool collected = false;
        private int lerpTime = 10;
        private int requiredIndex;
        private bool move = false;
        private bool collectedCountSent = false;
        [SerializeField] private TextMesh collectibleTextMesh;
        #endregion

        private void Awake()
        {
            requiredIndex = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());
        }

        private void Update()
        {
            if (!collected)
            {
                collectibleTextMesh.text = CollectedCount.ToString() + "/" + RequiredCollectibleCount.ToString();
            }
            else
            {
                collectibleTextMesh.text = string.Empty;
            }

            // 
            if (move)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), lerpTime * Time.deltaTime);
            }

            // avoid multiple count sending
            if (collectedCountSent)
            {
                GameManager.Instance.TotalCoins += CollectedCount;
                collectedCountSent = false;
            }
            
        }

        public IEnumerator CollectingCheck()
        {
            while (!collected)
            {
                yield return new WaitForSeconds(3.5f);

                if (CollectedCount >= RequiredCollectibleCount)
                {
                    move = true;
                    SoundManager.Instance.PlaySuccessClip(0);
                    transform.GetChild(0).GetComponent<Animation>().Play();
                    yield return new WaitForSeconds((lerpTime + 5) * Time.deltaTime);
                    PickerController.Instance.Playable = true;
                    transform.GetChild(0).GetComponent<Animation>().clip = null;
                    collectedCountSent = true;
                    UIManager.Instance.ChangeIndicator(requiredIndex - 1);
                    collected = true;
                }
                else
                {
                    collected = true;
                    SoundManager.Instance.PlaySuccessClip(1);
                    UIManager.Instance.EndScreen(1, 0);
                }

            }
        }
    }
}
