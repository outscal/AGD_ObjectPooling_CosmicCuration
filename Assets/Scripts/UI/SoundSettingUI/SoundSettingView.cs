using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmicCuration.UI
{
    public class SoundSettingView : MonoBehaviour
    {
        private SoundSettingController controller;

        [SerializeField] private Button SoundBtn;
        [SerializeField] private Button MusicBtn;
        [SerializeField] private Button backButton;

        private void Awake()
        {
            SoundBtn.onClick.AddListener(SoundBtnClicked);
            MusicBtn.onClick.AddListener(MusicBtnClicked);
            backButton.onClick.AddListener(BackBtnClicked);
        }

        public void SetController(SoundSettingController controllerToSet) => controller = controllerToSet;

        private void BackBtnClicked()=> controller.OnClickBackBtn();

        private void MusicBtnClicked() => controller.OnClickMusicBtn();

        private void SoundBtnClicked() => controller.OnClickSoundBtn();

    }
}