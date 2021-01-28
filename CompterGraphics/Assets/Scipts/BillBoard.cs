using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Camera m_cCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UV: 텍스쳐 좌표
        //빌보드: 카메라를 바라보는 평면
        //텍스쳐: 보여주는 이미지
        //행렬: 회전행렬은 11~33까지의 원소가 회전행렬이다.
        this.transform.LookAt(m_cCamera.transform);

        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;

        material.mainTextureOffset = new Vector2(0, 0);
        material.mainTextureScale = new Vector2(1, 1);
    }
}
