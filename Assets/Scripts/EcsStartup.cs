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

namespace Scripts
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        [SerializeField] PlayerInputData inputData;
        [SerializeField] SceneData sceneData;
        [SerializeField] StaticData staticData;
        [SerializeField] EcsUiEmitter emitter;
        [SerializeField] PathfindingData pathfindingData;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.ConvertScene();

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

        void AddGameplaySystems(EcsSystems systems)
        {
            systems
                .Add(new PlayerInputSystem())
                .Add(new CameraMoveSystem())
                .Add(new HudSystem())
                .Add(new ScenarioSystem())
                .Add(new PathfindingSystem())
                ;
        }

        void AddSpawnSystems(EcsSystems systems)
        {
            systems
                .Add(new EnemySpawnSystem())
                .Add(new BuildTowerSystem())
                .Add(new GameObjectSpawnSystem())
                .Add(new GameObjectDestroySystem())
                ;
        }

        void AddTowerSystems(EcsSystems systems)
        {
            systems
                .Add(new LaserSystem())
                .Add(new MortarSystem())
                .Add(new ProjectileSystem())
                ;
        }

        void AddMiscSystems(EcsSystems systems)
        {
            systems
                .Add(new EnemyMoveSystem())
                .Add(new DamageSystem())
                ;
        }

        void Inject(EcsSystems systems)
        {
            systems
                .Inject(inputData)
                .Inject(sceneData)
                .Inject(staticData)
                .Inject(pathfindingData);
        }

        void Update() 
        {
            _systems?.Run();
        }

        void OnDestroy() 
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
}