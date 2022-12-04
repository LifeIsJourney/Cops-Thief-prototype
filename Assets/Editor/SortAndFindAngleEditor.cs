using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SortAndFindAngle))]
public class SortAndFindAngleEditor : Editor
{
    private void OnSceneGUI()
    {
        GUIStyle stype = new GUIStyle()
        {
            normal = new GUIStyleState()
            {
                textColor = Color.grey
            }
        };
        GUIStyle highLight = new GUIStyle()
        {
            normal = new GUIStyleState()
            {
                textColor = Color.green
            }
        };

        SortAndFindAngle m_target = (SortAndFindAngle)target;

        List<TransformPointsWithAngle> pos = m_target.m_pWithAngle;
        if (m_target.m_drawLine)
            for (int i = 0; i < pos.Count; i++)
            {

                if (pos[i].IsLargestAngle)
                {
                    Handles.color = Color.green;
                    if (i != pos.Count - 1)
                    {
                        Handles.DrawLine(pos[i].p1.position, pos[i].p2.position);
                    }
                    else
                    {
                        Handles.DrawLine(pos[i].p1.position, pos[0].p1.position);
                    }
                    Handles.Label(pos[i].p1.position, $" points {pos[i].p1.name},{pos[i].p2.name}\n Angle {pos[i].angle}", highLight);

                    Handles.DrawLine(m_target.transform.position, pos[i].p1.position);
                    Handles.DrawLine(m_target.transform.position, pos[i].p2.position);

                    Handles.color = Color.blue;
                    Vector2 escapePoint = (pos[i].p1.position + pos[i].p2.position) / 2;
                    escapePoint.Normalize();
                    Handles.DrawLine(m_target.transform.position,escapePoint*7 ,5);
                }
                else
                {
                    Handles.color = Color.gray;

                    if (i != pos.Count - 1)
                    {
                        Handles.DrawLine(pos[i].p1.position, pos[i].p2.position);
                    }
                    else
                    {
                        Handles.DrawLine(pos[i].p1.position, pos[0].p1.position);
                    }
                    Handles.Label(pos[i].p1.position, $" points {pos[i].p1.name},{pos[i].p2.name}\n Angle {pos[i].angle}", stype);

                    Handles.DrawLine(m_target.transform.position, pos[i].p1.position);
                }
            }
    }
}
