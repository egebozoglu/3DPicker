using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Picker3D.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public static UIManager Instance;

        [Header("Screens")]
        public List<GameObject> Screens;

        [Header("Game Screen Variables")]
        [SerializeField] private TextMeshProUGUI gameCoinText; // in game coin amount
        [SerializeField] private TextMeshProUGUI currentLevel;
        [SerializeField] private TextMeshProUGUI nextLevel;
        [SerializeField] private List<Image> indicators = new List<Image>();
        private Color indicatorColor = new Color(1, 0.5f, 0, 1);

        [Header("End Screen Variables")]
        [SerializeField] private List<GameObject> endScreens = new List<GameObject>();
        [SerializeField] private TextMeshProUGUI endCoinText; // level end coin amount

        // level
        private int level;
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

        private void Start()
        {
            // set level texts
            level = PlayerPrefs.GetInt("Level");
            currentLevel.text = level.ToString();
            nextLevel.text = (level + 1).ToString();
        }

        #region Screens
        public void StartScreen()
        {
            ActivatingScreen(0);
        }

        public void GameScreen()
        {
            // activate screen and set coin amount
            ActivatingScreen(1);
            gameCoinText.text = PlayerPrefs.GetInt("CoinAmount").ToString();
        }

        public IEnumerator EndScreen(int index, int coinAmount)
        {
            yield return new WaitForSeconds(1);
            // activate screen, decide screen type and set coin amount
            ActivatingScreen(2);
            endScreens[index].SetActive(true);
            endCoinText.text = coinAmount.ToString();
        }

        private void ActivatingScreen(int index)
        {
            // activating screen depends on index
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
        #endregion

        public void ChangeIndicator(int index)
        {
            // change indicator color
            indicators[index].color = indicatorColor;
        }

        #region End Button Function
        public void EndButtonClicks(Button button)
        {
            if (button.name.Contains("Next"))
            {
                PlayerPrefs.SetInt("Level", level + 1);
            }

            SceneManager.LoadScene("GameScene");
        }
        #endregion
    }
}