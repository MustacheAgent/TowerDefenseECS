using Assets.Scripts.MonoLinks;
using Leopotam.Ecs;

namespace Assets.Scripts
{
    public class GameObjectMonoLink : MonoLink<GameObjectLink>
    {
        public override void Make(ref EcsEntity entity)
        {
            entity.Get<GameObjectLink>() = new GameObjectLink
            {
                Value = gameObject
            };
        }
    }
}
