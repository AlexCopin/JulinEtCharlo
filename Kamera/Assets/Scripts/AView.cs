using UnityEngine;

namespace Kamera
{
    internal abstract class AView : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsActiveOnStart { get; private set; }

        [field: Range(0, 1)]
        [field: SerializeField]
        public float Weight { get; private set; }

        public abstract CameraConfiguration Configuration { get; }

        public void SetActive(bool isActive)
        {
            if (isActive)
                CameraController.Instance.AddView(this);
            else
                CameraController.Instance.RemoveView(this);
        }

        private void Awake() => SetActive(IsActiveOnStart);

        private void OnDrawGizmos() => Configuration.DrawGizmos(Color.magenta);
    }
}