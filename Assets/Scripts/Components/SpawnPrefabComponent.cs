using System;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [Serializable]
    public struct SpawnPrefabComponent
    {
        public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Parent;
    }
}
