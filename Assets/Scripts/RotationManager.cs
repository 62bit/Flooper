using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotationManager : MonoBehaviour
{
    public List<GameObject> m_levels;
    private float[] m_rotCounts;

    private void Awake()
    {
        m_levels = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if(m_levels.Count < 1)
            return;
        
        foreach (GameObject level in m_levels)
        {
            int index = m_levels.IndexOf(level);
            level.transform.Rotate(new Vector3(0, 0, m_rotCounts[index]), 0.1f * m_rotCounts[index]);
        }
    }

    public void RemoveLevel(GameObject level)
    {
        m_levels.Remove(level);
    }

    public void SubmitLevel()
    {
        m_rotCounts = new float[m_levels.Count];
        for (int i = 0; i < m_rotCounts.Length; i++)
        {
            while (true)
            {
                float temp = Random.Range(-5, 5);
                if (temp != 0)
                {
                    m_rotCounts[i] = temp;
                    break;
                }
            }
        }
        GC.Collect();
    }
}
