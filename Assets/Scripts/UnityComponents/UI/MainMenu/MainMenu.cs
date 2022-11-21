using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityComponents.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private const string SceneName = "TestScene";
        
        public Button playButton;

        public void PlayPressed()
        {
            SceneManager.LoadScene(SceneName);
        }

        public void QuitPressed()
        {
            Application.Quit();
        }
    }
}