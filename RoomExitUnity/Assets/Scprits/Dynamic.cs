using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic : MonoBehaviour
{
    public float m_fSpeed;
    public float m_fJumpPower;
    public List<ItemManager.E_ITEM> m_listInventory;

    public int m_nTimmerCount = 0;
    public int m_nMaxTimmer = 180;

    public bool CheckGameOver()
    {
        return (m_nTimmerCount >= m_nMaxTimmer);
    }

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

    public bool ExitIventoy(ItemManager.E_ITEM item)
    {
        //람다식: 무명함수
        return m_listInventory.Exists( x =>  x == item );
    }
    //객체 활성화 전에 호출됨.
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float m_fTimeCounter;

    // Update is called once per frame
    void Update()
    {
        m_fTimeCounter += Time.deltaTime;
        if (m_fTimeCounter >= 1)
        {
            m_fTimeCounter -= 1;
            m_nTimmerCount++;
        }

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

       

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.up * m_fJumpPower);
        } 
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            float fDist = 10.0f;
            //레이어 충돌체크시에 필요한 레이어만 충돌체크하도록 설정할수있다.
            int nLayer = 1 << LayerMask.NameToLayer("RoomObject");
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * fDist, Color.red);
            if (Physics.Raycast(ray, out raycastHit, fDist, nLayer))
            {
                GameObject objCollision = raycastHit.collider.gameObject;
                Debug.Log(objCollision.name);

                RoomObject roomObject = objCollision.GetComponent<RoomObject>();
                if (roomObject &&
                    roomObject.tag == "ClickEvent")
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
            if (objCollison.tag == "CollisionEvent")
            {
                RoomObject roomObject = objCollison.GetComponent<RoomObject>();
                if (roomObject)
                    roomObject.CheckItem(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objCollison = other.gameObject;
        Debug.Log("TriggeEnter:" + objCollison.name);
        if (objCollison)
        {
            if (objCollison.tag == "TriggerEvent")
            {
                RoomObject roomObject = objCollison.GetComponent<RoomObject>();
                if (roomObject)
                    roomObject.CheckItem(this);
            }
        }
    }
}
