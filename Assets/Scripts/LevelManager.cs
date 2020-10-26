using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public int m_ringCount;
    public float m_bulletSpeedScale;
    public float m_bulletSizeScale;
    public float m_endTime;
}

public class LevelManager
{
    public static int m_currentLevel = 1;
    public const int m_levelCount = 4;
    public const int m_addRingPerLevel = 1;
    public const int  m_initRingLevel = 5;
    public const float m_bulletScalePerRing = 0.5f;
    public const float m_initTime = 20.0f;
    public const float m_initCameraOrthoScale = 50.0f;
    public const float m_cameraOrthoScale = 40.0f;
    public const float m_initRingScale = 10.0f;
    public const float m_initBallCoorY = 3.04f;
    public const float m_initGunScale = 10.0f;
    public const float m_initBulletSpeed = 400.0f;

    public static LevelInfo GetLevel(int level)
    {
        if (level > m_levelCount)
            return null;
        m_currentLevel = level;
        LevelInfo nLevel = new LevelInfo();
        nLevel.m_ringCount = m_initRingLevel + (m_addRingPerLevel * level);
        nLevel.m_bulletSpeedScale = 10f * level;
        nLevel.m_bulletSizeScale = 0.001f * (float)level;
        nLevel.m_endTime = m_initTime + (level * 5.0f);

        return nLevel;
    }

}
