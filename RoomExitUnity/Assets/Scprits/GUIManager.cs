using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public enum E_SCENCE_STATUS{ TITILE, GAMEOVER, THEEND, PLAY, MAX }
    public E_SCENCE_STATUS m_eCurStatus;
    public List<GameObject> m_listScence;

    public Text m_textTimmer;

    public void SetTimmer(int curTime, int maxTime)
    {
        m_textTimmer.text = string.Format("남은시간: {0}", curTime);
    }

    void ShowScence(E_SCENCE_STATUS status)
    {
        for(int i = 0; i< (int)E_SCENCE_STATUS.MAX; i++)
        {
            if (i == (int)status)
                m_listScence[i].SetActive(true);
            else
                m_listScence[i].SetActive(false);
        }
    }

    public void SetStatus(int statusNumber)
    {
        SetStatus((E_SCENCE_STATUS)statusNumber);
    }

    public void SetStatus(E_SCENCE_STATUS status)
    {
        switch (status)
        {
            case E_SCENCE_STATUS.TITILE:
                break;
            case E_SCENCE_STATUS.GAMEOVER:
                break;
            case E_SCENCE_STATUS.THEEND:
                break;
            case E_SCENCE_STATUS.PLAY:
                break;
        }
        ShowScence(status);
        m_eCurStatus = status;
    }

    public void UpdateStatus()
    {
        switch(m_eCurStatus)
        {
            case E_SCENCE_STATUS.TITILE:
                break;
            case E_SCENCE_STATUS.GAMEOVER:
                break;
            case E_SCENCE_STATUS.THEEND:
                break;
            case E_SCENCE_STATUS.PLAY:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_listScence = new List<GameObject>(transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform transformChild = transform.GetChild(i);
            Debug.Log("transformChild:"+i);
            m_listScence.Add(transformChild.gameObject);
        }
        SetStatus(m_eCurStatus);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatus();
    }
}
