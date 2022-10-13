using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Picker3D.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public static UIManager Instance;

        [Header("Screens")]
        public List<GameObject> Screens;

        [Header("Game Screen Texts")]
        [SerializeField] private TextMeshProUGUI gameCoinText; // in game coin amount

        [Header("End Screen Variables")]
        [SerializeField] private List<GameObject> endScreens = new List<GameObject>();
        [SerializeField] private TextMeshProUGUI endCoinText; // level end coin amount
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
            gameCoinText.text = PlayerPrefs.GetInt("CoinAmount").ToString();
        }

        public void EndScreen(int index, int coinAmount)
        {
            ActivatingScreen(2);
            endScreens[index].SetActive(true);
            endCoinText.text = coinAmount.ToString();
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