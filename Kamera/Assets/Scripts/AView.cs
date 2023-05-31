using UnityEngine;

namespace Kamera
{
    internal abstract class AView : MonoBehaviour
    {
        public bool IsActiveOnStart;

        public float Weight;

        public abstract CameraConfiguration GetConfiguration();

        public void SetActive(bool isActive)
        {
            if (isActive)
                CameraController.Instance.AddView(this);
            else
                CameraController.Instance.RemoveView(this);
        }

        private void Awake() => SetActive(IsActiveOnStart);

        private void OnDrawGizmos() => GetConfiguration().DrawGizmos(Color.magenta);
    }
}