using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : DoorObject
{
    void Start()
    {
        m_cDoor.m_bLook = true;    
    }
    public override bool CheckItem(Dynamic dynamic)
    {
        //플레이어가 해당아이템을 가지고있다면, 문을 락을 풀고 열쇠가 삭제된다.
        if(dynamic.ExitIventoy(m_eItem))
        {
            m_cDoor.m_bLook = false;
            dynamic.DeleteIventoryItem(m_eItem);
        }

        return false;
    }
}

