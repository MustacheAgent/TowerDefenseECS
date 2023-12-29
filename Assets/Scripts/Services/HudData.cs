using TMPro;
using UI;
using UnityComponents.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class HudData : MonoBehaviour
    {
        public Slider progressBar;
        public TextMeshProUGUI waveNumber;
        public TextMeshProUGUI currency;
        public TextMeshProUGUI baseHealth;

        public GameOverMenu gameOverMenu;
    }
}