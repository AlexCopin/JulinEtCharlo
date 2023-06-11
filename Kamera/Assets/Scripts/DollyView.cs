using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamera
{
    internal class DollyView : AView
    {
        public float Roll, Distance, Fov, speed;
        public GameObject Target;
        public Rail Rail;
        private float DistanceOnRail;

        private readonly Rect m_guiSlider = new Rect(10, 50, 150, 70);

        private Vector3 dir => Vector3.Normalize(Target.transform.position - transform.position);

        public override CameraConfiguration Configuration => new CameraConfiguration
        {
            Yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg,
            Pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg,
            Roll = Roll,
            Fov = Fov,
            Pivot = transform.position,
            Distance = Distance //...
        };


        private void Update()
        {
            DistanceOnRail += Input.GetAxis("Horizontal");

            if (DistanceOnRail > 1f)
            {
                DistanceOnRail = 0f;
            }
            else if (DistanceOnRail < 0f)
            {
                DistanceOnRail = 1f;
            }
        }
    }
}

