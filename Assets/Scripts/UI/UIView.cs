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
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button quitButton;
        #endregion

        #region Variables
        private int currentScore;
        #endregion

        private void Start()
        {
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
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnPlayAgainClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        private void OnQuitClicked() => Application.Quit();
    } 
}