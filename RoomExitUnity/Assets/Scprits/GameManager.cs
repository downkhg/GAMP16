using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dynamic m_cPlayer;
    public GUIManager m_cGuiManager;

    public Dictionary<string,RoomObject> m_listRoomObject;

    public RoomObject GetRoomObject(string objname)
    {
        return m_listRoomObject[objname];
    }

    public void InitRoomObject()
    {
        m_listRoomObject = new Dictionary<string, RoomObject>();
        RoomObject[] roomObjects = GameObject.FindObjectsOfType<RoomObject>();
        for (int i = 0; i < roomObjects.Length; i++)
        {
            Debug.Log("RoomObject["+i+"]:"+roomObjects[i].gameObject.name);
            m_listRoomObject.Add(roomObjects[i].gameObject.name, roomObjects[i]);
        }
    }

    static GameManager m_cInstance;

    public static GameManager GetInstance()
    {
        return m_cInstance;
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_cInstance = this;
        Debug.Log("GameManager::Awake()");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void EventCheckGameOver()
    {
        if (m_cPlayer.CheckGameOver())
            m_cGuiManager.SetStatus(GUIManager.E_SCENCE_STATUS.GAMEOVER);
    }

    void EventCheckTheEnd()
    {
        ExitDoor exitDoor = (ExitDoor)GetRoomObject("ExitDoor");

        if(exitDoor.CheckOpenDoor())
            m_cGuiManager.SetStatus(GUIManager.E_SCENCE_STATUS.THEEND);
    }
}
