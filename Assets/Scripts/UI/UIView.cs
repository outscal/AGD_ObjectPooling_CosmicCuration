using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CosmicCuration.UI
{
    public class UIView : MonoBehaviour
    {
        #region References
        [SerializeField] private Text scoreText;
        [SerializeField] private Text highScoreText;
        [SerializeField] private Text healthText;
        [SerializeField] private GameObject gameplayPanel;
        
        [Header("GameOver Panel")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text currentScoreTxt;
        [SerializeField] private Text highScoreTxt;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button quitButton;
        #endregion

        #region Variables
        private int currentScore;
        private int highScore;
        #endregion

        private void Start()
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                highScore= PlayerPrefs.GetInt("HighScore");
                highScoreText.text = highScore.ToString();
            }
    
            currentScore = 0;
            IncrementScore(currentScore);
        }

        public void IncrementScore(int scoreToIncrement)
        {
            currentScore += scoreToIncrement;
            scoreText.text = currentScore.ToString();
        }

        public void UpdateHealthUI(int healthToDisplay) => healthText.text = healthToDisplay.ToString();

        public void EnableGameOverUI()
        {
            gameplayPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            SetupGameOverPanelInfo();
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            quitButton.onClick.AddListener(OnMainMenuClicked);
        }

        private void SetupGameOverPanelInfo()
        {
            currentScoreTxt.text = "Your Score: "+currentScore;
            highScoreTxt.text = "High Score: "+highScore;
        }

        public void StoreHighScore()
        {
            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }

        private void OnPlayAgainClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        private void OnMainMenuClicked() => Application.Quit();
    } 
}