using UnityEngine;
using System.Collections.Generic;

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

        public Camera Camera { get; private set; }
        public CameraConfiguration CameraConfiguration { get; private set; }

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

        public void ApplyConfiguration(in Camera camera, in CameraConfiguration cameraConfiguration)
        {
            Camera = camera;
            CameraConfiguration = cameraConfiguration;
        }


    }
}