using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityComponents.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private const string SceneName = "TestScene";

        public LevelSelect levels;

        public Canvas mainMenuCanvas;

        public void PlayPressed()
        {
            mainMenuCanvas.enabled = false;
            levels.ShowMenu();
        }

        public void QuitPressed()
        {
            Application.Quit();
        }
    }
}