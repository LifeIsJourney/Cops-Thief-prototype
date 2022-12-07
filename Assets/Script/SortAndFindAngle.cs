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
            InstantiateCopsAndSetNonOverlapingPosition();
            m_findAngle = true;
        }
        if (m_findAngle)
        {
            m_findAngle = false;

            FindTheAngle(MathHelper.SortThePositionBasedOnOrigin(m_copsTransformList.ToArray(),m_thiefPos));
        }
    }

    void FindTheAngle(Transform[] _sortedPosArr)
    {
        m_pWithAngle = new List<TransformPointsWithAngle>();
        int largestAngleIndex = 0;
        for (int i = 0; i < _sortedPosArr.Length; i++)
        {
            if (i == _sortedPosArr.Length - 1)
            {
                 m_pWithAngle.Add(new TransformPointsWithAngle(_sortedPosArr[i], _sortedPosArr[0],
                    MathHelper.AngleUsingUnityVector2(m_thiefPos, _sortedPosArr[i].position, _sortedPosArr[0].position)));
            }
            else
            {
                m_pWithAngle.Add(new TransformPointsWithAngle(_sortedPosArr[i], _sortedPosArr[i+1],
                     MathHelper.AngleUsingUnityVector2(m_thiefPos, _sortedPosArr[i].position, _sortedPosArr[i+1].position)));
            }

            if (m_pWithAngle[i].angle > m_pWithAngle[largestAngleIndex].angle)
                largestAngleIndex = i;
        }
        m_pWithAngle[largestAngleIndex].IsLargestAngle = true;
    }
}
