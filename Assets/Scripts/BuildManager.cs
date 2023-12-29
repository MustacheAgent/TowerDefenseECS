using Components.Factory;
using Enums;
using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;
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
        if (Physics.Raycast(ray, out var hit))
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
            Prefab = sceneData.towerDictionary[type],
            Parent = null,
            Rotation = Quaternion.identity,
            Position = _selectedTile.transform.position
        };
        
        factoryData.factory.CreateObjectAndEntity(spawn);
        _selectedTile.walkable = false;
        _selectedTile.isBuildable = false;
        WorldHandler.GetWorld().NewEntity().Get<FindPathEvent>();
        
        buildMenu.HideMenu();
    }
}