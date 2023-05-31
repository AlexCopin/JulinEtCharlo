using UnityEngine;

namespace Kamera
{
    internal abstract class AView : MonoBehaviour
    {
        public float weight;

        public virtual CameraConfiguration GetConfiguration() => null;
    }
}