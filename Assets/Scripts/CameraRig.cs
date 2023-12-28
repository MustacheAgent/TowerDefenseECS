using System;
using UnityEngine;

namespace UnityComponents
{
    public class CameraRig : MonoBehaviour
    {
        private Transform _swivel, _stick;
        
        [HideInInspector]
        public Rect mapSize = new Rect(-10, -10, 20, 20);

        [Header("Zoom")] 
        public float stickMinZoom = -250f;
        public float stickMaxZoom = -45f;
        public float swivelMinZoom = 90f;
        public float swivelMaxZoom = 45f;
        private float _zoom = 1f;

        [Header("Movement")] 
        public float moveSpeedMinZoom = 400f;
        public float moveSpeedMaxZoom = 100f;

        private void Awake()
        {
            _swivel = transform.GetChild(0);
            _stick = _swivel.GetChild(0);
        }

        private void Update()
        {
            var zoomDelta = Input.GetAxis("Mouse ScrollWheel");
            if (zoomDelta != 0f) 
            {
                AdjustZoom(zoomDelta);
            }
            
            var xDelta = Input.GetAxis("Horizontal");
            var zDelta = Input.GetAxis("Vertical");
            if (xDelta != 0f || zDelta != 0f) 
            {
                AdjustPosition(xDelta, zDelta);
            }
        }

        private void AdjustZoom(float delta)
        {
            _zoom = Mathf.Clamp01(_zoom + delta);
            
            var distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, _zoom);
            _stick.localPosition = new Vector3(0f, 0f, distance);
            
            var angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, _zoom);
            _swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
        }

        private void AdjustPosition(float xDelta, float zDelta)
        {
            var damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
            var distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, _zoom) * Time.deltaTime * damping;
            var direction = new Vector3(xDelta, 0f, zDelta).normalized;
            
            var position = transform.localPosition;
            position += direction * distance;
            transform.localPosition = ClampPosition(position);
        }

        private Vector3 ClampPosition(Vector3 position)
        {
            var xMax = mapSize.x + mapSize.width;
            var zMax = mapSize.y + mapSize.height;

            position.x = Mathf.Clamp(position.x, mapSize.x, xMax);
            position.z = Mathf.Clamp(position.z, mapSize.y, zMax);
            return position;
        }
    }
}
