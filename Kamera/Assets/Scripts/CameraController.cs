using UnityEngine;

namespace Kamera
{
    internal class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance => _instance ??= new GameObject().AddComponent<CameraController>();

        public Camera Camera { get; private set; }
        public CameraConfiguration CameraConfiguration { get; private set; }

        private void Awake()
        {
            if (_instance is null)
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