using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Manager
{
    public class SoundManager : MonoBehaviour
    {
        #region Variables
        public static SoundManager Instance;
        [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
        [SerializeField] private AudioSource audioSource;
        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void PlaySuccessClip(int index)
        {
            audioSource.clip = clips[index];
            audioSource.Play();
        }
    }
}