using Assets.Scripts.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        SceneData _sceneData;

        void Start () 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            AddSystems();
            AddOneFrames();
            AddInjects();

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

        void AddSystems()
        {
            _systems
                .Add(new KeyInputSystem())
                .Add(new SpawnSystem());
        }

        void AddOneFrames()
        {

        }

        void AddInjects()
        {
            _systems
                .Inject(_sceneData);
        }

        void Update () 
        {
            _systems?.Run();
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