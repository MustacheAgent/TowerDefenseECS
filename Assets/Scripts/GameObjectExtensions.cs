using Leopotam.Ecs;
using System;
using UnityEngine;
using Voody.UniLeo;

namespace Scripts
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Получает сущность с объекта.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns>Сущность.</returns>
        /// <exception cref="NullReferenceException">У данного объекта нет сущности.</exception>
        public static EcsEntity? GetEntity(this GameObject gameObject)
        {
            var data = gameObject.GetComponent<ConvertToEntity>();
            if (data == null)
            {
                data = gameObject.GetComponentInParent<ConvertToEntity>();
            }

            return data.TryGetEntity();
        }
    }
}
