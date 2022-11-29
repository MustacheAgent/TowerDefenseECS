using Scenarios;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents.UI
{
    public class LevelSelect : MonoBehaviour
    {
        public Canvas mainMenuCanvas;
        public Canvas levelSelectCanvas;
        public LevelList levels;

        public LayoutGroup layout;

        public LevelButton buttonPrefab;

        private void Start()
        {
            foreach (var level in levels.levels)
            {
                LevelButton button = Instantiate(buttonPrefab, layout.transform);
                button.Initialize(level);
            }
            
            levelSelectCanvas.enabled = false;
        }

        public void ShowMenu()
        {
            levelSelectCanvas.enabled = true;
        }

        public void HideMenu()
        {
            mainMenuCanvas.enabled = true;
            levelSelectCanvas.enabled = false;
        }
    }
}