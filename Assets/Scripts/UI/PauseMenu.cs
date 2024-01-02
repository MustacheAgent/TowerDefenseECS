using System;
using Scenarios;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityComponents.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private const string MainMenuScene = "MainMenu";

        public Canvas pauseMenuCanvas;

        public Scenario levelScenario;
        
        public Button closeButton;
        public Button restartButton;
        public Button menuButton;

        public TextMeshProUGUI levelName;
        public TextMeshProUGUI levelDescription;

        private void Awake()
        {
            SetPauseMenuCanvas(false);
        }

        private void Start()
        {
            levelName.text = levelScenario.levelName;
            levelDescription.text = levelScenario.levelDescription;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Pause()
        {
            OpenPauseMenu();
            Time.timeScale = 0f;
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(MainMenuScene);
        }

        public void Unpause()
        {
            Time.timeScale = 1f;
            ClosePauseMenu();
        }

        private void SetPauseMenuCanvas(bool enable)
        {
            pauseMenuCanvas.enabled = enable;
        }

        private void OpenPauseMenu()
        {
            SetPauseMenuCanvas(true);
            /*
            closeButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            */
        }

        private void ClosePauseMenu()
        {
            SetPauseMenuCanvas(false);
            /*
            closeButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            */
        }
    }
}