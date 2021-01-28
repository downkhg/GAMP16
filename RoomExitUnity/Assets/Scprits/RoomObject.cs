using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public ItemManager.E_ITEM m_eItem;
  
    public virtual bool CheckItem(Dynamic dynamic)
    {
        if (m_eItem != ItemManager.E_ITEM.NONE)
        {
            dynamic.SetInventory(m_eItem);
            m_eItem = ItemManager.E_ITEM.NONE;
            return false;
        }

        return false;
    }
}
