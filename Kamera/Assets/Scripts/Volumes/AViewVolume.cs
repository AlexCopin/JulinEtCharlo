using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamera
{
    internal abstract class AViewVolume : MonoBehaviour
    {
        public int Priority { get; set; }
        public AView View { get; set; }

        private int UID;

        private static int NextUID;

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

        public virtual float ComputeSelfWeight() => 1.0f;

        private void Awake()
        {
            UID = NextUID;
            NextUID++;
        }

    }
}
