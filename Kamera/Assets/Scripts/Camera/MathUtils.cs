using UnityEngine;

internal static class MathUtils
{
    public static Vector3 LinearBezier(Vector3 a, Vector3 b, float t)
        => (1 - t) * a + t * b;

    public static Vector3 QuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t)
        => Mathf.Pow(1 - t, 2) * a +
           2 * (1 - t) * t * b +
           Mathf.Pow(t, 2) * c;

    public static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        => Mathf.Pow(1 - t, 3) * a +
           3 * Mathf.Pow(1 - t, 2) * t * b +
           3 * (1 - t) * Mathf.Pow(t, 2) * c +
           Mathf.Pow(t, 3) * d;
}