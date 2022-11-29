﻿using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class LocalPosition
    {
        [MenuItem("Debug/Print Global Position")]
        public static void PrintGlobalPosition()
        {
            if (Selection.activeGameObject != null)
            {
                Debug.Log(Selection.activeGameObject.name + " is at " + Selection.activeGameObject.transform.position);
            }
        }
    }
}