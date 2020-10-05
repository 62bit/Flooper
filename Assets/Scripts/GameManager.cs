using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ringPrefab;
    [SerializeField] private GameObject m_ballPrefab;
    [SerializeField] private RotationManager m_rotationManager;
    private UIManager m_uiManager;
    private GameObject m_levelContainer;
    private GameObject m_gun;
    private GameObject m_ball;

    private float m_remTime;

    private static GameManager m_instance = null;

    public static GameManager Instance
    {
        get
        {
            if (m_instance != null)
                return m_instance;
            
            return null;
        }
    }

    private void Awake()
    {
        m_instance = this;
        m_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        m_levelContainer = GameObject.Find("Levels");
        m_gun = GameObject.Find("Gun");
        m_ball = GameObject.Find("ball");
        m_rotationManager = GameObject.Find("RotationManager").GetComponent<RotationManager>();
    }

    void Start()
    {
        CreateNewLevel(LevelManager.m_currentLevel);
    }

    private void Update()
    {
        if(m_remTime >= 0)
        {
            m_uiManager.SetTime(m_remTime = m_remTime - Time.deltaTime);
        }
        else
        {

            m_uiManager.ShowLoseMenu();
            m_gun.GetComponent<Gun>().m_isFireOpen = false;
        }

    }

    public void CreateNewLevel(int level)
    {
        m_uiManager.ShowLevelImage(level);
        m_ball.transform.position = new Vector3(0.0f, LevelManager.m_initBallCoorY, m_ball.transform.position.z);
        m_ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        m_uiManager.ShowLevelImage(level);
        m_rotationManager.m_levels.Clear();
        LevelInfo lInfo = LevelManager.GetLevel(level);

        m_remTime = lInfo.m_endTime;

        Gun gun = m_gun.GetComponent<Gun>();
        gun.m_isFireOpen = true;
        gun.m_bulletSize = lInfo.m_bulletSizeScale;
        gun.m_bulletSpeed = LevelManager.m_initBulletSpeed + lInfo.m_bulletSpeedScale;
        m_gun.transform.localScale = new Vector3(LevelManager.m_initGunScale, LevelManager.m_initGunScale, 1.0f);
        m_ball.transform.localPosition = new Vector3(0.0f, LevelManager.m_initBallCoorY, 0.0f);
        CameraZoomControl.Instance.m_newOrthoSize = LevelManager.m_initCameraOrthoScale;

        for(int i = 0; i < lInfo.m_ringCount; i++)
        {
            GameObject ring = Instantiate(m_ringPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ring.transform.SetParent(m_levelContainer.transform);
            float scale = LevelManager.m_initRingScale * (i+1);
            ring.transform.localScale = new Vector3(scale, scale, 1f);
            m_rotationManager.m_levels.Add(ring);
        }


        m_rotationManager.SubmitLevel();
    }

    public void RestartGame()
    {

        foreach(var item in m_rotationManager.GetComponent<RotationManager>().m_levels)
        {
            Destroy(item);
        }
        m_rotationManager.GetComponent<RotationManager>().m_levels.Clear();

        m_remTime = 10.0f;
        m_uiManager.ShowLevelImage(1);
        LevelManager.m_currentLevel = 1;
        CreateNewLevel(LevelManager.m_currentLevel);

    }

    public void StopGame()
    {

    }
}
