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
        [field: SerializeField] private bool IsAuto;
        private float DistanceOnRail;
        private float Yaw => Mathf.Atan2(dir.x, dir.z)* Mathf.Rad2Deg;
        private float Pitch => -Mathf.Asin(dir.y) * Mathf.Rad2Deg;


        private readonly Rect m_guiSlider = new Rect(10, 50, 150, 70);

        private Vector3 dir => Vector3.Normalize(Target.transform.position - /*CameraController.Instance.*/transform.position);

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
            if (IsAuto) DistanceOnRail += speed * Time.deltaTime;
            else DistanceOnRail += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            if (!Rail.IsLoop) 
            {
                if (DistanceOnRail > 1f)
                {
                    if (Rail.GetCurrentNode() != Rail.GetRailNodeSize())
                    {
                        Rail.UpdateNode(1);
                        DistanceOnRail = 0f;
                    }
                    else DistanceOnRail = 1f;
                }
                else if (DistanceOnRail < 0f)
                {
                    if (Rail.GetCurrentNode() != 0)
                    {
                        Rail.UpdateNode(-1);
                        DistanceOnRail = 1f;
                    }
                    else DistanceOnRail = 0f;
                }
            }
            else
            {
                if (DistanceOnRail > 1f)
                {
                    Rail.UpdateNode(1);
                    DistanceOnRail = 0f;
                }
            }
            
            CameraController.Instance.SetTransform(Rail.GetPosition(DistanceOnRail), transform.rotation);
        }
    }
}

