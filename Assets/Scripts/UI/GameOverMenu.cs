using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverMenu : MonoBehaviour
    {
        public Canvas menuCanvas;
        public TextMeshProUGUI title;
        public TextMeshProUGUI hint;
        public Button nextLevel;

        private const string HintFailed = "Don't give up, you can do it.";
        private const string HintCompleted = "Nice job mate!";
        
        private const string LevelFailed = "LEVEL FAILED!";
        private const string LevelCompleted = "LEVEL COMPLETED!";

        private const string MainMenu = "MainMenu";
        
        private void Start()
        {
            menuCanvas.enabled = false;
            nextLevel.gameObject.SetActive(false);
        }

        public void ShowMenu(bool isVictory)
        {
            Time.timeScale = 0f;
            title.text = isVictory ? LevelCompleted : LevelFailed;
            hint.text = isVictory ? HintCompleted : HintFailed;
            menuCanvas.enabled = true;
        }

        public void MainMenuPressed()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(MainMenu);
        }
        
        public void RestartPressed()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}