using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
    public float m_fSpeed;

    public List<ItemManager.E_ITEM> m_listInventory;

    public void SetInventory(ItemManager.E_ITEM item)
    {
        m_listInventory.Add(item);
    }

    public ItemManager.E_ITEM GetIventory(int idx)
    {
        return m_listInventory[idx];
    }

    public void DeleteIventoryItem(ItemManager.E_ITEM item)
    {
        m_listInventory.Remove(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * m_fSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * m_fSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * 1);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            float fDist = 10.0f;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction*fDist, Color.red);
            if(Physics.Raycast(ray,out raycastHit,fDist))
            {
                GameObject objCollision = raycastHit.collider.gameObject;
                Debug.Log(objCollision.name);

                RoomObject roomObject = objCollision.GetComponent<RoomObject>();
                if (roomObject && 
                    roomObject.tag == "Book")
                    roomObject.CheckItem(this);
            }
        }

    }

    private void OnGUI()
    {
        int idx = 0;
        foreach(ItemManager.E_ITEM item in m_listInventory)
        {
            GUI.Box(new Rect(0, 20 * idx, 100, 20), item.ToString());
            idx++;
        }
    }
    //충돌체끼리 물리리연산으로 충돌할때 호출되는 함수.
    private void OnCollisionEnter(Collision collision)
    {
       GameObject  objCollison =  collision.gameObject;
        Debug.Log("CollisonEnter:"+objCollison.name);
        if(objCollison)
        {
            RoomObject roomObject = objCollison.GetComponent<RoomObject>();
            if(roomObject)
                roomObject.CheckItem(this);
        }
    }
}
