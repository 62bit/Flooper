using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private GameObject m_cannon;
    [SerializeField] private Flash m_flashImage;

    private ParticleSystem m_particleSystem;
    private ParticleSystem m_levelUpParticles;
    private Transform m_bulletTransform;
    private Camera m_camera;
    private AudioSource m_audioSource;
    private AudioSource m_levelUpAudio;
    
    private Vector2 m_lookAtMouse;

    public float m_bulletSpeed;
    public float m_bulletSize;

    public bool m_isFireOpen;

    void Start()
    {
        m_bulletTransform = m_bullet.transform;
        m_camera = Camera.main;
        m_particleSystem = m_cannon.GetComponent<ParticleSystem>();
        m_particleSystem.Stop();
        m_audioSource = m_cannon.GetComponent<AudioSource>();
        m_levelUpAudio = this.gameObject.GetComponent<AudioSource>();
        m_levelUpParticles = this.gameObject.GetComponent<ParticleSystem>();
        m_levelUpParticles.Stop();
        m_flashImage = GameObject.Find("Image").GetComponent<Flash>();
        m_isFireOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isFireOpen)
        {
            m_lookAtMouse = (m_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            RotateGun();
            if(Input.GetMouseButtonDown(0))
            {
                StartCoroutine(StartFire());
            }
        }
        
    }
    void FireBullet()
    {
        if(m_isFireOpen)
        {
            m_flashImage.CameraFlash();
            var temp = Instantiate(m_bullet, new Vector3(0, 0, 1), Quaternion.identity);
            temp.transform.localScale = new Vector3(temp.transform.localScale.x + m_bulletSize, temp.transform.localScale.x + m_bulletSize, temp.transform.localScale.z);
            temp.GetComponent<Rigidbody2D>().velocity += m_lookAtMouse * m_bulletSpeed;
            StartCoroutine(nameof(StartParticles));
        }
        
    }

    void RotateGun()
    {
        float rot_z = Mathf.Atan2(m_lookAtMouse.y, m_lookAtMouse.x) * Mathf.Rad2Deg;
        m_cannon.transform.rotation = Quaternion.Euler(0f,0f, rot_z - 90);
    }

    IEnumerator StartParticles()
    {
        m_particleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        m_particleSystem.Stop();
    }
    public IEnumerator StartLevelUpParticles()
    {
        m_levelUpParticles.Play();
        yield return new WaitForSeconds(0.5f);
        m_levelUpParticles.Stop();
    }

    public IEnumerator StartFire()
    {
        PlayFireAudio();
        FireBullet();
        yield return new WaitForSeconds(0.1f);
        PlayFireAudio();
        FireBullet();
    }

    void PlayFireAudio()
    {
        m_audioSource.Play();
    }

    public void GunLevelUp(int size)
    {
        m_levelUpAudio.Play();
        Vector3 temp = this.gameObject.transform.localScale;
        this.gameObject.transform.localScale = new Vector3(temp.x + size , temp.y +size , temp.z);
    }
}
