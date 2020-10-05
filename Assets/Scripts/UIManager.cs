using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_images;
    [SerializeField] private GameObject m_winMenu;
    [SerializeField] private GameObject m_loseMenu;
    [SerializeField] private Text m_timeText;

    public void ShowLevelImage(int index)
    {
        if(index > m_images.Count)
            return;

        m_images[index].GetComponent<Image>().color = new Color
            (m_images[index].GetComponent<Image>().color.r,
            m_images[index].GetComponent<Image>().color.g,
            m_images[index].GetComponent<Image>().color.b,
            255.0f
            );
        m_images[index].SetActive(true);
    }

    public void ShowWinMenu()
    {
        m_winMenu.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        m_loseMenu.SetActive(true);   
    }

    public void PressPlayAgainButton()
    {
        m_winMenu.SetActive(false);
        m_loseMenu.SetActive(false);
        GameManager.Instance.RestartGame();
    }

    public void SetTime(float time)
    {
        float seconds = Mathf.FloorToInt(time % 60);

        m_timeText.text = "Time :" + seconds.ToString() + " sec";
    }
}
