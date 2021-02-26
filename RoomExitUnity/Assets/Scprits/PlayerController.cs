using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Dynamic dynamicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dynamicPlayer.Translate(transform, Vector3.forward);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dynamicPlayer.Translate(transform, Vector3.back);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dynamicPlayer.Rotation(transform, Vector3.up);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dynamicPlayer.Rotation(transform, Vector3.down);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            dynamicPlayer.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dynamicPlayer.Jump(GetComponent<Rigidbody>());
        }
    }

    //private void FixedUpdate()
    //{
    //    int nLayer = 0;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit raycastHit;
    //        float fDist = 10.0f;
    //        //레이어 충돌체크시에 필요한 레이어만 충돌체크하도록 설정할수있다.
    //        nLayer = 1 << LayerMask.NameToLayer("RoomObject");
    //        Debug.DrawLine(ray.origin, ray.origin + ray.direction * fDist, Color.red);
    //        if (Physics.Raycast(ray, out raycastHit, fDist, nLayer))
    //        {
    //            GameObject objCollision = raycastHit.collider.gameObject;
    //            Debug.Log(objCollision.name);

    //            RoomObject roomObject = objCollision.GetComponent<RoomObject>();
    //            if (roomObject &&
    //                roomObject.tag == "ClickEvent")
    //                roomObject.CheckItem(dynamicPlayer);
    //        }
    //    }

    //    dynamicPlayer.m_colliderTarget = dynamicPlayer.ProcessFindNearCollider("Monster");
    //}
}
