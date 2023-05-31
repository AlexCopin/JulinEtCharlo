using UnityEngine;

namespace Kamera
{
    internal class FixedView : AView
    {
        [Range(0, 360)]
        public float Yaw;
        [Range(-90, 90)]
        public float Pitch;
        [Range(-180, 180)]
        public float Roll;
        [Range(10, 150)]
        public float Fov;

        public override CameraConfiguration GetConfiguration() => new CameraConfiguration
        {
            Yaw = Yaw,
            Pitch = Pitch,
            Roll = Roll,
            Fov = Fov,
            Pivot = transform.position,
            Distance = 0
        };
    }
}