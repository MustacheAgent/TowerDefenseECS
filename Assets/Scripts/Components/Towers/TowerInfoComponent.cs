using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Towers
{
    [Serializable]
    public struct TowerInfoComponent
    {
        public Sprite towerIcon;
        public int towerPrice;
        public Sprite energyIcon;
        public string towerName;
    }
}
