using Leopotam.Ecs;
using Systems.Core;
using UnityEngine;
using Voody.UniLeo;

namespace Assets.Scripts
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        EcsSystems _fixedSystems;

        [SerializeField]
        PlayerInputData _inputData;
        [SerializeField]
        SceneData _sceneData;
        [SerializeField]
        StaticData _staticData;

        void Start () 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _fixedSystems = new EcsSystems(_world);
            _systems.ConvertScene();
            _fixedSystems.ConvertScene();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new PlayerInputSystem())
                .Add(new CameraMoveSystem())
                .Add(new SpawnSystem())
                .Add(new EnemySpawnSystem())
                .Add(new MoveSystem())
                ;

            _systems
                .Inject(_inputData)
                .Inject(_sceneData)
                .Inject(_staticData);

            _systems.Init();
            _fixedSystems.Init();



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

        void Update () 
        {
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        void OnDestroy () 
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