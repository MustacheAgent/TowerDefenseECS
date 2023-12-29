using ECS.MonoProviders.Components.Towers;
using Services;
using UnityEngine;

namespace UI
{
    public class BuildMenu : MonoBehaviour
    {
        [SerializeField] private SceneData data;
        [SerializeField] private Canvas menuCanvas;
        [SerializeField] private BuildTowerButton towerButton;
        [SerializeField] private BuildManager buildManager;

        private void Start()
        {
            foreach (var tower in data.towerDictionary)
            {
                var button = Instantiate(towerButton, transform);
                button.InitButton(tower.Value.GetComponent<TowerInfoProvider>().Value, tower.Key, buildManager);
            }
            
            menuCanvas.enabled = false;
        }

        public void ShowMenu()
        {
            menuCanvas.enabled = true;
        }
        
        public void HideMenu()
        {
            menuCanvas.enabled = false;
        }
    }
}
