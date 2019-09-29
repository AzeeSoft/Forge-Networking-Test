using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperExtensions
{
    #region Transform

    public static Bounds TransformBounds(this Transform self, Bounds bounds)
    {
        var center = self.TransformPoint(bounds.center);
        var points = bounds.GetCorners();

        var result = new Bounds(center, Vector3.zero);
        foreach (var point in points)
            result.Encapsulate(self.TransformPoint(point));
        return result;
    }

    public static Bounds InverseTransformBounds(this Transform self, Bounds bounds)
    {
        var center = self.InverseTransformPoint(bounds.center);
        var points = bounds.GetCorners();

        var result = new Bounds(center, Vector3.zero);
        foreach (var point in points)
            result.Encapsulate(self.InverseTransformPoint(point));
        return result;
    }

    #endregion

    #region Bounds

    public static List<Vector3> GetCorners(this Bounds obj, bool includePosition = true)
    {
        var result = new List<Vector3>();
        for (int x = -1; x <= 1; x += 2)
        for (int y = -1; y <= 1; y += 2)
        for (int z = -1; z <= 1; z += 2)
            result.Add((includePosition ? obj.center : Vector3.zero) + (obj.size / 2).Times(new Vector3(x, y, z)));
        return result;
    }

    #endregion

    #region Vector3

    public static Vector3 Times(this Vector3 self, Vector3 other)
    {
        return new Vector3(self.x * other.x, self.y * other.y, self.z * other.z);
    }

    #endregion
}