using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SortAndFindAngle : ObjectInstantiator
{
    [HideInInspector]
    public List<TransformPointsWithAngle> m_pWithAngle;
    [Header("Draw line in scene view")]
    public bool m_drawLine = true;

    bool m_findAngle;

   
    void Update()
    {
        if (m_relaodCops)
        {
            m_relaodCops = false;
            InstantiateCopsAndSetPosition();
            m_findAngle = true;
        }
        if (m_findAngle)
        {
            m_findAngle = false;

            FindTheAngle(SortThePosition());
        }
    }

    Transform[] SortThePosition()
    {
        Transform[] tmp_posTransArr = m_copsTransformList.ToArray();
        m_pWithAngle = new List<TransformPointsWithAngle>();

        ClockwiseComparer cc = new ClockwiseComparer(m_thiefPos);
        Array.Sort(tmp_posTransArr, cc);
        return tmp_posTransArr;
    }

    void FindTheAngle(Transform[] _posArr)
    {
        int largestAngleIndex = 0;
        for (int i = 0; i < _posArr.Length; i++)
        {
            if (i == _posArr.Length - 1)
            {
                 m_pWithAngle.Add(new TransformPointsWithAngle(_posArr[i], _posArr[0],
                     AngleUsingUnityVector2(m_thiefPos, _posArr[i].position, _posArr[0].position)));
            }
            else
            {
                m_pWithAngle.Add(new TransformPointsWithAngle(_posArr[i], _posArr[i+1],
                     AngleUsingUnityVector2(m_thiefPos, _posArr[i].position, _posArr[i+1].position)));
            }

            if (m_pWithAngle[i].angle > m_pWithAngle[largestAngleIndex].angle)
                largestAngleIndex = i;
        }
        m_pWithAngle[largestAngleIndex].IsLargestAngle = true;
    }

    public float AngleUsingUnityVector2(Vector2 _myPos, Vector2 _firstVector, Vector2 _secVector)
    {
        Vector2 firstLine = new Vector2(_myPos.x - _firstVector.x, _myPos.y - _firstVector.y);
        Vector2 secLine = new Vector2(_myPos.x - _secVector.x, _myPos.y - _secVector.y);
        return Vector2.Angle(firstLine, secLine);
    }
}
