using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTransparent : MonoBehaviour
{

    private Image m_image;
    public float m_disappearTime;
    void Start()
    {
        m_image = GetComponent<Image>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if(m_image.color.a > 0.001)
            m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b,
                                   Mathf.Lerp(m_image.color.a, 0.0f, Time.deltaTime * m_disappearTime));
        else
            this.gameObject.SetActive(false);
    }
}
