using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper
{
    static int m_gapBetweenTwoCircle = 1;
    static int m_dontExceedThisLimit = 2000;
   
    static bool IsCircleOverlaping(Vector2 _first, Vector2 _sec)
    {
        //d = sqrt[ (x1-x2)^2 + (y1-y2)^2 ]
        float disX = _first.x - _sec.x;
        float disY = _first.y - _sec.y;
        float d = Mathf.Sqrt(disX * disX + disY * disY);
        if (d < 1 + m_gapBetweenTwoCircle) return true; //d = r1+r2 for now assume 1
        return false;
    }

    public static Vector2[] GetNonOverlappingPoints(int _noOfPoints, Vector2 _addThiefPoint)
    {
        List<Vector2> points = new List<Vector2>();
        int runTime = 0;
        bool isOverlap;
        Vector2 currPoint;

        points.Add(_addThiefPoint);
        for (int i = 1; i <= _noOfPoints; i++)
        {
            do
            {
                isOverlap = false;
                currPoint = GiveRandomNumber();
                for (int j = 0; j < points.Count; j++)
                {
                    Vector2 other = points[j];

                    if (IsCircleOverlaping(other, currPoint))
                    { isOverlap = true; break; }
                }
                runTime++;
                if (runTime > m_dontExceedThisLimit) { Debug.LogError("cannot find a point reduce circle count or gap"); isOverlap = false; break; }
            } while (isOverlap);

            if (!isOverlap && runTime <= m_dontExceedThisLimit)
                points.Add(currPoint);
        }
        return points.ToArray();
    }

   
    static Vector2 GiveRandomNumber(float _x = 4,float _y=4)
    {
        return new Vector2(Random.Range(-_x, _x),Random.Range(-_y,_y));
    }
}
