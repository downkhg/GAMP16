using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : RoomObject
{
    [SerializeField]
    Door m_cDoor; 
    public override bool CheckItem(Dynamic dynamic)
    {
        if(m_cDoor.m_cDoorAsix.m_bOpen)
            return base.CheckItem(dynamic);
        return false;
    }
}
