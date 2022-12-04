using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransformPointsWithAngle
{
    public Transform p1, p2;
    public float angle;
    public bool IsLargestAngle;

    public TransformPointsWithAngle(Transform _p1, Transform _p2, float _angle)
    {
        p1 = _p1;
        p2 = _p2;
        angle = _angle;
        IsLargestAngle = false;
    }
}
