using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamera
{
    internal class Rail : MonoBehaviour
    {
        public bool IsLoop;

        [SerializeField] private bool RenderGizmos;

        [SerializeField][Tooltip("The color used to draw the lines")] private Color GizmoLineColorBase;
        [SerializeField][Tooltip("The color used to draw the line the camera is currently on")] private Color GizmoLineColorCurrent;

        private float m_length;

        private List<Vector3> m_nodes = new List<Vector3>();

        private void Start()
        {
            for (int i = 0; i < transform.childCount; ++i)
                m_nodes.Add(transform.GetChild(i).position);

            m_length = ComputeRailLength();
        }

        private float ComputeRailLength()
        {
            float l = 0;
            for (int i = 0; i < m_nodes.Count - 2; ++i) l += Vector3.Distance(m_nodes[i], m_nodes[i + 1]);
            if (IsLoop) l += Vector3.Distance(m_nodes[m_nodes.Count - 1], m_nodes[0]);
            return l;
        }

        public float GetLength() => m_length;

        Vector3 GetPosition(float distance)
        {
            return Vector3.zero;
        }



        private void OnDrawGizmos()
        {
            if (RenderGizmos) DrawGizmos();
        }


        private void DrawGizmos()
        {
            Gizmos.color = GizmoLineColorBase;
            for (int i = 0; i < m_nodes.Count - 2; ++i) Gizmos.DrawLine(m_nodes[i], m_nodes[i + 1]);
            if (IsLoop) Gizmos.DrawLine(m_nodes[m_nodes.Count - 1], m_nodes[0]);
        }
    }
}

