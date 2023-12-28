using System;
using Components.Towers;
using Enums;
using Leopotam.Ecs.Ui.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents.UI
{
    public class BuildTowerButton : MonoBehaviour
    {
        public TowerType towerType;
        public TextMeshProUGUI price;
        public Image towerIcon;
        public string towerName;
        public Button buyButton;
        public Image energyIcon;
        public Color energyDefaultColor;
        public Color energyInvalidColor;

        public void InitButton(TowerInfoComponent info, TowerType type)
        {
            price.text = info.towerPrice.ToString();
            towerIcon.sprite = info.towerIcon;
            towerType = type;
        }
    }
}