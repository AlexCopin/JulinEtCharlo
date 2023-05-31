using System.Collections.Generic;
using UnityEngine;

namespace Kamera
{
    internal class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance
            => _instance
            = _instance != null
            ? _instance
            : new GameObject(nameof(CameraController)).AddComponent<CameraController>();

        [field: SerializeField] public Camera Camera { get; private set; }

        private readonly List<AView> _activeViews = new List<AView>();

        public void AddView(in AView view) => _activeViews.Add(view);
        public void RemoveView(in AView view) => _activeViews.Remove(view);

        private void ApplyConfig(in CameraConfiguration cameraConfiguration)
        {
            Camera.transform.SetPositionAndRotation(cameraConfiguration.Position, cameraConfiguration.Rotation);
            Camera.fieldOfView = cameraConfiguration.Fov;
        }

        private CameraConfiguration ComputeAverage()
        {
            var yawSum = Vector2.zero;
            var picthSum = Vector2.zero;
            var rollSum = Vector2.zero;
            var fovSum = 0f;
            var weightSum = 0f;

            foreach (var view in _activeViews)
            {
                var cfg = view.GetConfiguration();

                yawSum += new Vector2(Mathf.Cos(cfg.Yaw * Mathf.Deg2Rad), Mathf.Sin(cfg.Yaw * Mathf.Deg2Rad)) * view.Weight;
                picthSum += new Vector2(Mathf.Cos(cfg.Pitch * Mathf.Deg2Rad), Mathf.Sin(cfg.Pitch * Mathf.Deg2Rad)) * view.Weight;
                rollSum += new Vector2(Mathf.Cos(cfg.Roll * Mathf.Deg2Rad), Mathf.Sin(cfg.Roll * Mathf.Deg2Rad)) * view.Weight;
                fovSum += cfg.Fov * view.Weight;
                weightSum += view.Weight;
            }

            var yaw = Vector2.SignedAngle(Vector2.right, yawSum);
            var pitch = Vector2.SignedAngle(Vector2.right, picthSum);
            var roll = Vector2.SignedAngle(Vector2.right, rollSum);
            var fov = fovSum / weightSum;

            return new CameraConfiguration { Yaw = yaw, Pitch = pitch, Roll = roll, Fov = fov };
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                return;
            }

            Destroy(this);
        }

        private void Update() => ApplyConfig(ComputeAverage());
    }
}