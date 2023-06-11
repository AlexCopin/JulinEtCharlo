using UnityEngine;

namespace Kamera
{
    internal abstract class AViewVolume : MonoBehaviour
    {
        public int Priority { get; private set; }
        public AView View { get; private set; }

        private static int NextUID;

        private int UID;

        private bool _isActive;

        protected bool IsActive
        {
            get => _isActive;
            set
            {
                if (value) ViewVolumeBlender.Instance.AddVolume(this);
                else ViewVolumeBlender.Instance.RemoveVolume(this);
                _isActive = value;
            }
        }

        public virtual float ComputeSelfWeight() => 1;

        private void Awake()
        {
            UID = NextUID;
            NextUID++;
        }
    }
}