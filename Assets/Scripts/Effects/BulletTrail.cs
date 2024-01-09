using System.Collections;
using UnityEngine;

namespace Effects
{
    public class BulletTrail : MonoBehaviour
    {
        [HideInInspector] public Vector3 start, end;
        
        private TrailRenderer _trail;
        private float _time;
        
        private void Start()
        {
            _trail = GetComponent<TrailRenderer>();
            _time = 0f;
            start = transform.position;
        }

        private void Update()
        {
            if (_time < 1)
            {
                transform.position = Vector3.Lerp(start, end, _time);
                _time += Time.deltaTime / _trail.time;
            }
            else
            {
                _trail.transform.position = end;
                Destroy(gameObject);
            }
        }
        
        private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 start, Vector3 end)
        {
            var time = 0f;

            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(start, end, time);
                time += Time.deltaTime / trail.time;

                yield return null;
            }

            trail.transform.position = end;
        }
        
    }
}
