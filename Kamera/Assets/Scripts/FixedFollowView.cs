using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamera
{
    internal class FixedFollowView : AView
    {
        [Range(-180, 180)]
        public float Roll;
        [Range(10, 150)]
        public float Fov;

        [Range(0, 360)]
        public float YawOffsetMax;
        [Range(-90, 90)]
        public float PitchOffsetMax;

        public GameObject Target;
        public GameObject CentralPoint;


        private Vector3 dir => GetDirNorm();

        public override CameraConfiguration Configuration => new CameraConfiguration
        {
            Yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg,
            Pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg,
            Roll = Roll,
            Fov = Fov,
            Pivot = transform.position,
            Distance = 0
        };

        private Vector3 GetDirNorm() => Vector3.Normalize(Target.transform.position - transform.position);

    }

}
