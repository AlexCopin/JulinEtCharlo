using System;
using UnityEngine;

namespace Kamera
{
    internal abstract class AView : MonoBehaviour
    {
        public float weight;

        public bool isActiveOnStart;

        private void Start() => SetActive(isActiveOnStart);



        public virtual CameraConfiguration GetConfiguration() => throw new NotImplementedException();

        public void SetActive(in bool isActive) => gameObject.SetActive(isActive);
    }
}