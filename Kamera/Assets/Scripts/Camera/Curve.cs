using UnityEngine;

internal class Curve
{
    public Vector3 a, b, c, d;

    public Vector3 GetPosition(float t) => MathUtils.CubicBezier(a, b, c, d, t);

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        var aWorld = localToWorldMatrix.MultiplyPoint(a);
        var bWorld = localToWorldMatrix.MultiplyPoint(b);
        var cWorld = localToWorldMatrix.MultiplyPoint(c);
        var dWorld = localToWorldMatrix.MultiplyPoint(d);

        return MathUtils.CubicBezier(aWorld, bWorld, cWorld, dWorld, t);
    }

    public void DrawGizmos(Color c, Matrix4x4 localToWorldMatrix)
    {
    }
}