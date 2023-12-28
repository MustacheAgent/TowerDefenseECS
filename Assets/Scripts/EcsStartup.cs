using Events;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Services;
using Systems.Core;
using Systems.Enemies;
using Systems.Factory;
using Systems.Towers;
using Systems.UI;
using UnityEngine;
using Voody.UniLeo;

sealed class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;

    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private SceneData sceneData;
    [SerializeField] private FactoryData factoryData;
    [SerializeField] private EcsUiEmitter emitter;
    [SerializeField] private HudData hudData;
    [SerializeField] private GridData gridData;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        WorldHandler.Init(_world);
        //_systems.ConvertScene();

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
        //_systems.OneFrame<>

        AddGameplaySystems(_systems);
        AddSpawnSystems(_systems);
        AddTowerSystems(_systems);
        AddMiscSystems(_systems);

        Inject(_systems);
            
        _systems.InjectUi(emitter);

        OneFrame(_systems);

        _systems.Init();

        // register your systems here, for example:
        // .Add (new TestSystem1 ())
        // .Add (new TestSystem2 ())

        // register one-frame components (order is important), for example:
        // .OneFrame<TestComponent1> ()
        // .OneFrame<TestComponent2> ()

        // inject service instances here (order doesn't important), for example:
        // .Inject (new CameraService ())
        // .Inject (new NavMeshSupport ())
    }

    private void AddGameplaySystems(EcsSystems systems)
    {
        systems
            .Add(new ScenarioSystem())
            .Add(new PathfindingSystem())
            .Add(new GameProgressionSystem())
            ;
    }

    private void AddSpawnSystems(EcsSystems systems)
    {
        systems
            .Add(new EnemySpawnSystem())
            .Add(new BuildTowerSystem())
            .Add(new GameObjectSpawnSystem())
            .Add(new GameObjectDestroySystem())
            ;
    }

    private void AddTowerSystems(EcsSystems systems)
    {
        systems
            .Add(new TrackTargetSystem())
            .Add(new LaserSystem())
            .Add(new MortarTowerSystem())
            .Add(new MortarShellSystem())
            .Add(new RocketTowerSystem())
            .Add(new RocketSystem())
            ;
    }

    private void AddMiscSystems(EcsSystems systems)
    {
        systems
            .Add(new EnemyMoveSystem())
            .Add(new DamageSystem())
            .Add(new TimerSystem())
            .Add(new HudUpdateSystem())
            ;
    }

    private void Inject(EcsSystems systems)
    {
        systems
            .Inject(inputData)
            .Inject(sceneData)
            .Inject(factoryData)
            .Inject(hudData)
            .Inject(gridData)
            ;
    }

    private void OneFrame(EcsSystems systems)
    {
        systems
            .OneFrame<CurrencyChangedEvent>()
            .OneFrame<BaseHealthChangedEvent>()
            ;
    }

    private void Update() 
    {
        _systems?.Run();
    }

    private void OnDestroy() 
    {
        if (_systems != null) 
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}