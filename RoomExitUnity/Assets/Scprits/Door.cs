using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorAsix m_cDoorAsix;

    public void Open()
    {
        m_cDoorAsix.Open();
    }

    private void OnMouseDown()
    {
        m_cDoorAsix.Open();
    }
}
