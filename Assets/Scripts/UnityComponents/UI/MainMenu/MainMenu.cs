using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityComponents.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private const string SceneName = "TestScene";
        
        public Button PlayButton;

        public void OnPlayPressed()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}