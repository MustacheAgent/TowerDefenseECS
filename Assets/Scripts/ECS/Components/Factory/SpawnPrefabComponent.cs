using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECS.Components.Factory
{
    [Serializable]
    public struct SpawnPrefabComponent
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
    }
}
