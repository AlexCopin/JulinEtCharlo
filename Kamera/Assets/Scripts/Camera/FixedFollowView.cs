using UnityEngine;

namespace Kamera
{
    internal class FixedFollowView : AView
    {
        [Space]
        [Range(-180, 180)]
        public float Roll;
        [Range(10, 150)]
        public float Fov;
        [Space]
        [Range(0, 360)]
        public float YawOffsetMax;
        [Range(-90, 90)]
        public float PitchOffsetMax;
        [Space]
        [SerializeField] private GameObject Target;
        [SerializeField] private GameObject CentralPoint;

        private Vector3 Dir => (Target.transform.position - transform.position).normalized;

        public override CameraConfiguration Configuration => new CameraConfiguration
        {
            Yaw = Mathf.Atan2(Dir.x, Dir.z) * Mathf.Rad2Deg,
            Pitch = -Mathf.Asin(Dir.y) * Mathf.Rad2Deg,
            Roll = Roll,
            Fov = Fov,
            Pivot = transform.position,
            Distance = 0
        };
    }
}