using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuUI
{
    public class MainMenu : MonoBehaviour
    {
        #region References

        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject doubleTurretText;
        [SerializeField] private GameObject rapidFireText;


        #region MainMenu References

        [Header("MainMenu")]
        [SerializeField] private GameObject homeScreen;
        [SerializeField] private Button startButton;
        [SerializeField] private Button optionsButton;

        #endregion

        #region OptionsMenu References
        [Header("OptionsMenu")]
        [SerializeField] private GameObject optionsPanel;
        [SerializeField] private Button optionsPanelBackButton;
        [SerializeField] private Button audioButton;
        [SerializeField] private Button setDifficultyButton;
        [SerializeField] private Button instructionsButton;

        #endregion

        #region AudioMenu References
        [Header("AudioMenu")]
        [SerializeField] private GameObject audioPanel;
        [SerializeField] private Button audioPanelBackButton;
        [SerializeField] private Button soundButton;
        [SerializeField] private Button musicbutton;

        #endregion

        #region DifficultyMenu References
        [Header("DifficultyMenu")]
        [SerializeField] private GameObject difficultyPanel;
        [SerializeField] private Button difficultyPanelBackButton;
        [SerializeField] private Button easyButton;
        [SerializeField] private Button mediumButton;
        [SerializeField] private Button hardButton;

        #endregion

        #region InstructionMenu References
        [Header("InstructionsMenu")]
        [SerializeField] private GameObject instructionsPanel;
        [SerializeField] private Button instructionPanelBackButton;

        #endregion
        #endregion

        private void Awake()
        {
            startButton.onClick.AddListener(OnClickStartButton);
            optionsButton.onClick.AddListener(OnClickOptionsButton);
            optionsPanelBackButton.onClick.AddListener(OnClickOptionsPanelBackButton);
            audioButton.onClick.AddListener(OnClickAudioButton);
            setDifficultyButton.onClick.AddListener(OnClickSetDifficultyButton);
            instructionsButton.onClick.AddListener(OnClickInstructionsButton);
            audioPanelBackButton.onClick.AddListener(OnClickAudioPanelBackButton);
            soundButton.onClick.AddListener(OnClickSoundButton);
            musicbutton.onClick.AddListener(OnClickMusicButton);
            difficultyPanelBackButton.onClick.AddListener(OnClickDifficultyPanelBackButton);
            easyButton.onClick.AddListener(OnClickEasyButton);
            mediumButton.onClick.AddListener(OnClickMediumButton);
            hardButton.onClick.AddListener(OnClickHardButton);
            instructionPanelBackButton.onClick.AddListener(OnClickInstructionPanelBackButton);
        }

        private void OnClickStartButton()
        {
            homeScreen.SetActive(false);
            GameService.Instance.Initialize();
            gameplayPanel.SetActive(true);
        }

        private void OnClickOptionsButton()
        {
            homeScreen.SetActive(false);
            optionsPanel.SetActive(true);
        }

        private void OnClickOptionsPanelBackButton()
        {
            optionsPanel.SetActive(false);
            homeScreen.SetActive(true);
        }

        private void OnClickAudioButton()
        {
            optionsPanel.SetActive(false);
            audioPanel.SetActive(true);
        }

        private void OnClickSetDifficultyButton()
        {
            optionsPanel.SetActive(false);
            difficultyPanel.SetActive(true);
        }

        private void OnClickInstructionsButton()
        {
            optionsPanel.SetActive(false);
            instructionsPanel.SetActive(true);
        }

        private void OnClickAudioPanelBackButton()
        {
            audioPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }

        private void OnClickSoundButton()
        {
            GameService.Instance.GetSoundService().SetSoundState();
        }

        private void OnClickMusicButton()
        {
            GameService.Instance.GetSoundService().SetMusicState();
        }

        private void OnClickDifficultyPanelBackButton()
        {
            difficultyPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }

        private void OnClickEasyButton()
        {
            GameService.Instance.difficultyLevel = DifficultyLevel.easy;
        }

        private void OnClickMediumButton()
        {
            GameService.Instance.difficultyLevel = DifficultyLevel.medium;
        }

        private void OnClickHardButton()
        {
            GameService.Instance.difficultyLevel = DifficultyLevel.hard;
        }

        private void OnClickInstructionPanelBackButton()
        {
            instructionsPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }

        public void ToggleDoubleTurretText(bool value)
        {
            doubleTurretText.SetActive(value);
        }

        public void ToggleRapidFireText(bool value)
        {
            rapidFireText.SetActive(value);
        }
    }

    public enum DifficultyLevel
    {
        easy,
        medium,
        hard
    }
}

