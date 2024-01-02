using Components.Towers;
using ECS.Components.Towers;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildTowerButton : MonoBehaviour
    {
        [HideInInspector] public TowerType towerType;
        [HideInInspector] public string towerName;
        [HideInInspector] public int towerPrice;
        
        public Image towerIcon;
        public TextMeshProUGUI towerPriceText;
        public TextMeshProUGUI towerNameText;
        public Sprite energyIcon;
        public Color energyDefaultColor;
        public Color energyInvalidColor;

        private BuildManager _buildManager;

        public void InitButton(TowerInfoComponent info, TowerType type, BuildManager buildManager)
        {
            towerPrice = info.towerPrice;
            towerPriceText.text = towerPrice.ToString();
            
            towerIcon.sprite = info.towerIcon;
            towerType = type;
            
            towerName = info.towerName;
            towerNameText.text = towerName;

            _buildManager = buildManager;

            gameObject.GetComponent<Button>().onClick.AddListener(() => buildManager.BuildTower(type));
        }

        public void SetAvalaibility(bool isAvalaible)
        {
            if (isAvalaible)
            {
                
            }
        }
    }
}