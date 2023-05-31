using System;
using UnityEngine;

namespace Kamera
{
    [Serializable]
    internal class CameraConfiguration
    {
        [Range(0, 360)]
        public float Yaw;

        [Range(-90, 90)]
        public float Pitch;

        [Range(-180, 180)]
        public float Roll;

        [Range(10, 150)]
        public float Fov;

        public float Distance;

        public Vector3 Pivot;

        public Vector3 Offset => Rotation * (Vector3.back * Distance);
        public Vector3 Position => Pivot + Offset;
        public Quaternion Rotation => Quaternion.Euler(Pitch, Yaw, Roll);

        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(Pivot, .25f);
            var position = Position;
            Gizmos.DrawLine(Pivot, position);
            Gizmos.matrix = Matrix4x4.TRS(position, Rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, Fov, .5f, 0, Camera.main.aspect);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}