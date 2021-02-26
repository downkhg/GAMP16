using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Timmer
{
    public float m_fMaxTime;
    public float m_fCurTime;

    public Timmer(float maxTime = 10)
    {
        m_fCurTime = -1;
        m_fMaxTime = maxTime;
    }

    public void UpdateTimmer()
    {
        if (m_fCurTime > -1)
            m_fCurTime += Time.deltaTime;
    }

    public void StartTimmer()
    {
        m_fCurTime = 0;
    }

    public void StopTimmer()
    {
        m_fCurTime = -1;
    }

    public void ResetTimmer()
    {
        m_fCurTime -= m_fMaxTime;
    }

    public bool CheckTimmer(bool loop = true)
    {
        if (m_fCurTime >= m_fMaxTime)
        {
            if (loop)
                ResetTimmer();
            else
                StopTimmer();
            return true;
        }
        return false;
    }
}

public class AIController : MonoBehaviour
{
    [SerializeField]
    Dynamic m_dynamicPlayer;

    public enum E_AI_STATE { ATTAK, MOVE, SEARCH, LOOKAT }
    public E_AI_STATE m_eCurState;

    Timmer m_sAttackTimmer;

    public float m_fSite;

    public float m_fRotAngle;
    public float m_fCurRotAngle;
    public Vector3 m_vRotAsix;


    public void SetState(E_AI_STATE state)
    {
        switch (state)
        {
            case E_AI_STATE.ATTAK:
                m_sAttackTimmer = new Timmer(1);
                m_dynamicPlayer.Attack();
                m_sAttackTimmer.StartTimmer();
                break;
            case E_AI_STATE.MOVE:
                transform.LookAt(m_dynamicPlayer.m_colliderTarget.transform);
                break;
            case E_AI_STATE.SEARCH:

                break;
            case E_AI_STATE.LOOKAT:
                if (m_dynamicPlayer.m_colliderTarget)
                {
                    Vector3 vDir = transform.forward;
                    Vector3 vPos = transform.position;
                    Vector3 vTaregtPos = m_dynamicPlayer.m_colliderTarget.gameObject.transform.position;
                    Vector3 vToTarget = vTaregtPos - vPos;

                    float fAngle = Vector3.Angle(vDir, vToTarget.normalized);
                    float fDot = Vector3.Dot(vDir, vToTarget.normalized);
                    float fRad = Mathf.Acos(fDot);
                    if (fDot > 0)
                        m_vRotAsix = Vector3.up;
                    else
                        m_vRotAsix = Vector3.down;
                    m_fRotAngle = Mathf.Rad2Deg * fRad;
                }
                else
                {
                    SetState(E_AI_STATE.SEARCH);
                }
                break;
        }
        m_eCurState = state;
    }

    public void UpdateState()
    {
        switch (m_eCurState)
        {
            case E_AI_STATE.ATTAK:
                if (m_dynamicPlayer.m_colliderTarget)
                {
                    m_sAttackTimmer.UpdateTimmer();
                    if (m_sAttackTimmer.CheckTimmer())
                        m_dynamicPlayer.Attack();
                }
                else
                    SetState(E_AI_STATE.SEARCH);
                break;
            case E_AI_STATE.MOVE:
                if (m_dynamicPlayer.m_colliderTarget)
                {
                    Vector3 vPos = transform.position;
                    Vector3 vTargetPos = m_dynamicPlayer.m_colliderTarget.transform.position;

                    float fDist = Vector3.Distance(vPos, vTargetPos);
                    if (fDist > m_dynamicPlayer.m_fAttackRange)
                        m_dynamicPlayer.Translate(transform, Vector3.forward);
                    else
                        SetState(E_AI_STATE.ATTAK);
                }
                else
                {
                    SetState(E_AI_STATE.SEARCH);
                }
                break;
            case E_AI_STATE.SEARCH:
                if (m_dynamicPlayer.m_colliderTarget == null)
                {
                    m_dynamicPlayer.m_colliderTarget = ProcessFindNearCollider("Player");
                }
                else
                    SetState(E_AI_STATE.LOOKAT);
                break;
            case E_AI_STATE.LOOKAT:
                if (m_fCurRotAngle <= m_fRotAngle)
                {
                    m_dynamicPlayer.Rotation(transform, m_vRotAsix);
                    m_fCurRotAngle += m_dynamicPlayer.m_fAngleSpeed;
                }
                else
                {
                    SetState(E_AI_STATE.MOVE);
                }
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetState(m_eCurState);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, m_fSite);
    }

    public Collider ProcessFindNearCollider(string strLayerName)
    {
        int nLayer = 1 << LayerMask.NameToLayer(strLayerName);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, m_fSite, nLayer);

        Collider colliderMin = null;
        float fPreDist = 99999999.0f;
        //찾은대상중 가장 가까운 대상을 찾는다.
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            float fDist = Vector3.Distance(collider.transform.position, this.transform.position);

            if (colliderMin == null || fPreDist > fDist)
                colliderMin = collider;
            fPreDist = fDist;
            Debug.Log(string.Format("[{0}]{1}:{2}", i, collider.gameObject.name, fDist));
        }

        return colliderMin;
    }

}
