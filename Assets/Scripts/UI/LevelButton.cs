using Scenarios;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityComponents.UI
{
    public class LevelButton : MonoBehaviour
    {
        public TextMeshProUGUI levelName;
        public TextMeshProUGUI levelDescription;

        private string _sceneName;

        public void Initialize(Scenario scenario)
        {
            levelName.text = scenario.levelName;
            levelDescription.text = scenario.levelDescription;
            _sceneName = scenario.sceneName;
        }
        
        public void Click()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}