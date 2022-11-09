using System;
using Components.Towers;
using Leopotam.Ecs.Ui.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildTowerButton : MonoBehaviour
{
    public TextMeshProUGUI price;

    public Image towerIcon;

    public Button buyButton;

    public Image energyIcon;

    public Color energyDefaultColor;
		
    public Color energyInvalidColor;

    public void InitButton(TowerInfoComponent info)
    {
        price.text = info.towerPrice.ToString();
        towerIcon.sprite = info.towerIcon;
        EcsUiActionBase.AddAction<EcsUiClickAction>(gameObject);
    }
}
