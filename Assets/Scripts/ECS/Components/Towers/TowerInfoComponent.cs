using System;
using UnityEngine;

namespace ECS.Components.Towers
{
    [Serializable]
    public struct TowerInfoComponent
    {
        [Header("Build Parameters")]
        public Sprite towerIcon;
        public int towerPrice;
        public Sprite energyIcon;
        public string towerName;
    }
}
