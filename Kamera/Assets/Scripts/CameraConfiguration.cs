using System;
using UnityEngine;

namespace Kamera
{
    [Serializable]
    internal class CameraConfiguration
    {
        public float Yaw;
        public float Pitch;
        public float Roll;
        public float Distance;
        public float Fov;

        public Vector3 Pivot;

        public Vector3 Position => Pivot * Distance;
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