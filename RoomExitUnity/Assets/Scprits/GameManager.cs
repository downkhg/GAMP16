using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dynamic m_cPlayer;
    public GUIManager m_cGuiManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_cGuiManager.SetTimmer(m_cPlayer.m_nTimmerCount, m_cPlayer.m_nMaxTimmer);
    }
}
