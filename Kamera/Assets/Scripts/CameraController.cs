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

        private List<AView> activeViews = new List<AView>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                return;
            }

            Destroy(this);
        }

        public void ApplyConfiguration(in Camera camera)
        {
            Camera = camera;
        }

        public void AddView(AView view) => activeViews.Add(view);

        public void RemoveView(AView view) => activeViews.Remove(view);

        private void Update()
        {
            ComputeCamsAverage();
        }

        public float ComputeAverageYaw()
        {
            Vector2 sum = Vector2.zero;
            foreach (AView view in activeViews)
            {
                CameraConfiguration config = view.GetConfiguration();
                sum += new Vector2(Mathf.Cos(config.Yaw * Mathf.Deg2Rad),
                Mathf.Sin(config.Yaw * Mathf.Deg2Rad)) * view.weight;
            }
            return Vector2.SignedAngle(Vector2.right, sum);
        }

        private void ComputeCamsAverage()
        {
        }
    }
}