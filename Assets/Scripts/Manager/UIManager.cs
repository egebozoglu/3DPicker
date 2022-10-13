using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public static UIManager Instance;

        [Header("Screens")]
        public List<GameObject> Screens;
        #endregion

        private void Awake()
        {
            if (Instance==null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartScreen()
        {
            ActivatingScreen(0);
        }

        public void GameScreen()
        {
            ActivatingScreen(1);
        }

        public void EndScreen()
        {
            ActivatingScreen(2);
        }

        private void ActivatingScreen(int index)
        {
            for (int i = 0; i < Screens.Count; i++)
            {
                if (i==index)
                {
                    Screens[i].SetActive(true);
                }
                else
                {
                    Screens[i].SetActive(false);
                }
            }
        }
    }
}