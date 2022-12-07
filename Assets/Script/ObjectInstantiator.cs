using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    [SerializeField] GameObject m_copsPrefab, m_thiefPrefab;

    int m_copsCount = 10;

    protected Vector2 m_thiefPos = Vector2.zero;        //thief is the center

    protected List<Transform> m_copsTransformList;
    Vector2[] m_nonOverlappingPoints;

    [Header("Change random cop position")]
    public bool m_relaodCops;
    private void Start()
    {
        m_nonOverlappingPoints = MathHelper.GetNonOverlappingPoints(m_copsCount,m_thiefPos);
        InstantiateThief();
    }

    void InstantiateThief()
    {
        Instantiate(m_thiefPrefab, m_thiefPos,Quaternion.identity);
    }
    protected void InstantiateCopsAndSetNonOverlapingPosition()
    {
        if (m_copsTransformList == null || m_copsTransformList.Count != m_copsCount)
        {
            m_copsTransformList = new List<Transform>();
            m_nonOverlappingPoints = MathHelper.GetNonOverlappingPoints(m_copsCount,m_thiefPos);
            for (int i = 0; i < m_copsCount; i++)
            {
                m_copsTransformList.Add(InstantiateAtPosition(m_copsPrefab,
                    m_nonOverlappingPoints[i+1]));
            }
        }
        else
        {
            m_nonOverlappingPoints = MathHelper.GetNonOverlappingPoints(m_copsCount,m_thiefPos);
            for (int i = 0; i < m_copsCount; i++)
            {
                m_copsTransformList[i].position = m_nonOverlappingPoints[i+1];
            }
        }
    }
   
    private Transform InstantiateAtPosition(GameObject _prefab,Vector3 _position)
    {
        GameObject obj = Instantiate(_prefab, transform);
        obj.transform.position = _position;
        return obj.transform;
    }

}
