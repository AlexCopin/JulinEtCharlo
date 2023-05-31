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
        [field: SerializeField] public CameraConfiguration CameraConfiguration { get; private set; }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                return;
            }

            Destroy(this);
        }

        public void ApplyConfiguration(in Camera camera, in CameraConfiguration cameraConfiguration)
        {
            Camera = camera;
            CameraConfiguration = cameraConfiguration;
        }
    }
}