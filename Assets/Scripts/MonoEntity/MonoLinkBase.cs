using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class MonoLinkBase : MonoBehaviour
	{
		public abstract void Make(ref EcsEntity entity);
	}
}
