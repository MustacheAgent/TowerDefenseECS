using ECS.MonoProviders.Components.Towers;
using MonoProviders.Components.Towers;
using Services;
using UnityEngine;

namespace UI
{
    public class BuildSidebar : MonoBehaviour
    {
        [SerializeField] public BuildTowerButton towerButton;

        [SerializeField] public SceneData sceneData;
        
        // Start is called before the first frame update
        private void Start()
        {
            foreach (var tower in sceneData.towerDictionary)
            {
                var button = Instantiate(towerButton, transform);
                //button.InitButton(tower.Value.GetComponent<TowerInfoProvider>().Value, tower.Key);
            }
        }
    }
}