using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomControl : MonoBehaviour
{
    private static CameraZoomControl m_instance = null;
    
    [SerializeField] private float m_cameraZoomSpeed;
    public float m_newOrthoSize;
    public const float m_defaultOrthoSize = 25f;
    private Camera m_camera;

    public static CameraZoomControl Instance
    {
        get
        {
            if (m_instance == null)
                return null;
            else
            {
                return m_instance;
            }
        }
    }

    private void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        m_camera = this.gameObject.GetComponent<Camera>();
    }
    
    void Update()
    {
        if (m_camera.orthographicSize != m_newOrthoSize)
            m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, 
                                            m_newOrthoSize,
                                            m_cameraZoomSpeed * Time.deltaTime);
    }
}
