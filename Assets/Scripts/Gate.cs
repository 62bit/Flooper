using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject m_parent;
    private Gun m_gun;
    private RotationManager m_rotationManager;
    private UIManager m_uiManager;
    void Start()
    {
        m_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        m_parent = this.transform.parent.gameObject;
        m_rotationManager = GameObject.Find("RotationManager").GetComponent<RotationManager>();
        m_gun = GameObject.Find("Gun").GetComponent<Gun>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ball")
        {
            CameraZoomControl.Instance.m_newOrthoSize += LevelManager.m_cameraOrthoScale;
            m_gun.GunLevelUp(1);
            m_gun.m_bulletSpeed += 50.0f;
            m_rotationManager.RemoveLevel(m_parent);
            m_parent.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(m_gun.StartLevelUpParticles());
            StartCoroutine(PlayDestroyAnimationAndDestroy());
            m_gun.m_bulletSize += LevelManager.m_bulletScalePerRing;

            if (m_rotationManager.m_levels.Count == 0)
            {
                if (LevelManager.m_currentLevel == 3 ) 
                {
                    m_uiManager.ShowWinMenu();
                    GameManager.Instance.m_timerIsTicking = false;
                    return;
                }
                LevelManager.m_currentLevel++;
                GameManager.Instance.CreateNewLevel(LevelManager.m_currentLevel);
            }
        }
    }

    IEnumerator PlayDestroyAnimationAndDestroy()
    {
        for (int i = 0; i < 5; i++)
        {
            Color temp = m_parent.GetComponent<SpriteRenderer>().color;
            m_parent.GetComponent<SpriteRenderer>().color = new Color(226, 250, 74,  temp.a * 0.2f);
            yield return new WaitForSeconds(0.15f);
        }
        Destroy(m_parent);
    }
}
