using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(menuName = "Factories/UI Object Factory", fileName = "UIObjectFactory")]
    public class UIObjectFactory : ScriptableObject
    {
        public GameObject Spawn(GameObject button, Transform transform)
        {
            return Instantiate(button, transform);
        }

        public void Reclaim(GameObject destroyObject)
        {
            Destroy(destroyObject);
        }
    }
}