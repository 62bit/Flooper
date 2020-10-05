using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject m_centerObject;

    [SerializeField] private float m_rotationSpeed;
    private float m_timer;
    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        m_centerObject.transform.Rotate(new Vector3(0,0,1),m_rotationSpeed);
    }

    public void ScaleOrbitSize(float f)
    {
        transform.position = new Vector3(transform.position.x + f , transform.position.y +f ,transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bullet")
            m_audioSource.Play();
    }
}
