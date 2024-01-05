using ECS.Components.Factory;
using ECS.Components.Towers;
using Enums;
using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;
using Scripts;
using Services;
using UI;
using UnityEngine;
using Voody.UniLeo;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;
    [SerializeField] private FactoryData factoryData;
    [SerializeField] private BuildMenu buildMenu;

    private Tile _selectedTile;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue, 1))
        {
            var tile = hit.transform.GetComponent<Tile>();
            if (tile != null && tile.isBuildable)
            {
                _selectedTile = tile;
                buildMenu.ShowMenu();
            }
        }
    }

    public void BuildTower(TowerType type)
    {
        var spawn = new SpawnPrefabComponent
        {
            prefab = sceneData.towerDictionary[type],
            parent = null,
            rotation = Quaternion.identity,
            position = _selectedTile.transform.position
        };
        
        var tower = factoryData.factory.CreateObjectAndEntity(spawn);
        if (tower.GetEntity().HasValue && tower.GetEntity().Value.Has<TrackTargetComponent>())
        {
            tower.GetEntity().Value.Get<TrackTargetComponent>().canAttack = false;
        }
        
        _selectedTile.walkable = false;
        _selectedTile.isBuildable = false;
        WorldHandler.GetWorld().NewEntity().Get<FindPathEvent>();
        
        buildMenu.HideMenu();
    }
}