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
        public bool CollectingStarted = false;
        private int lerpTime = 10;
        [SerializeField] private TextMesh collectibleTextMesh;
        #endregion

        private void Update()
        {
            collectibleTextMesh.text = CollectedCount.ToString() + "/" + RequiredCollectibleCount.ToString();

            if (CollectingStarted)
            {
                StartCoroutine(CollectingCheck());
            }
        }

        public IEnumerator CollectingCheck()
        {
            yield return new WaitForSeconds(3f);

            if (CollectedCount>= RequiredCollectibleCount)
            {
                transform.GetChild(0).GetComponent<Animation>().Play();
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), lerpTime * Time.deltaTime);
                yield return new WaitForSeconds((lerpTime+1) * Time.deltaTime);
                PickerController.Instance.Playable = true;
                CollectingStarted = false;
                transform.GetChild(0).GetComponent<Animation>().clip = null;
            }
            else
            {
                // level failed
            }
        }
    }
}
