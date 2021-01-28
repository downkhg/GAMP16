using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectTest : MonoBehaviour
{
    public GameObject objReflect; //반사될 물체
    public float fSpeed;
    public Vector3 vDir; //반사될 물체의 방향
    public Plane cPlane; //반사될 평면

    // Start is called before the first frame update
    void Start()
    {

        Vector3 vA = new Vector3(0, 0, 0);
        Vector3 vB = new Vector3(1, 0, 0);
        Vector3 vC = new Vector3(0, 0, 1);

        Vector3 vReflectPos = objReflect.transform.position;
        Vector3 vPlanePos = cPlane.normal * cPlane.distance;
        vDir = vPlanePos - vReflectPos; //공이 있는 위치에서 평면 위치로 입사각을 계산한다.
        vDir.Normalize();
        cPlane = new Plane(vA,vC,vB);
    }
    public float fCosT;
    // Update is called once per frame
    void Update()
    {
        Vector3 vRelfectPos = objReflect.transform.position;

        vRelfectPos += vDir * fSpeed * Time.deltaTime;

        Vector3 vPlanePos = cPlane.normal * cPlane.distance;
        Vector3 vDist = vRelfectPos - vPlanePos;
        //vDir.Normalize();
        Vector3 vPlaneNormal = cPlane.normal;
        Debug.DrawLine(vRelfectPos, vRelfectPos + (vDir *2),Color.red);
        fCosT = Vector3.Dot(vPlaneNormal, vDist.normalized);
        if (fCosT <= 0)
        {
            //Debug.LogError("reflect");
            vDir = Vector3.Reflect(vDir, cPlane.normal);
        }

        objReflect.transform.position = vRelfectPos;
    }

    private void OnDrawGizmos()
    {
        Vector3 vStart = cPlane.normal * cPlane.distance;
        Vector3 vEnd = vStart + cPlane.normal * 1;

        Gizmos.DrawLine(vStart, vEnd);
    }
}
