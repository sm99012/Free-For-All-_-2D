using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public GameObject m_gCamera;
    public Camera m_Camera;
    [SerializeField]
    Transform m_tCameraTransform;

    [SerializeField]
    GameObject m_gPlayerCameraBox;
    [SerializeField]
    PlayerCameraBox m_PlayerCameraBox;


    // Camera 중심점.
    [SerializeField]
    Vector3 m_vCameraCenterPosition;
    Vector3 m_vGetCameraCenterPosition;
    // Z축 좌표.
    float m_fFixZ;
    // zoomin 시 최대 z축 좌표.
    float m_fZommInZ;

    public void InitialSet()
    {
        m_tCameraTransform = m_gCamera.GetComponent<Transform>();
        m_Camera = m_gCamera.GetComponent<Camera>();

        m_gPlayerCameraBox = this.gameObject.transform.Find("PlayerCameraBox").gameObject;
        m_PlayerCameraBox = m_gPlayerCameraBox.GetComponent<PlayerCameraBox>();

        m_vCameraCenterPosition = new Vector3();
        m_vGetCameraCenterPosition = new Vector3();

        m_fFixZ = -2.25f; // -1.75f
        m_fZommInZ = -1f;
    }

    // Camera 이동 관련 스크립트.
    // true: Player 이동 시 Player가 카메라의 중심에 위치.
    // false: 카메라 포지션 제한.
    string m_sJudgeStr;
    public bool CameraMove(Vector2 playerpos)
    {
        if (m_PlayerCameraBox.m_eCC == PlayerCameraBox.E_CAMERA_CATEGORY.NORMAL)
        {
            m_sJudgeStr = m_PlayerCameraBox.JudgeCameraCenterPosition();
            //if (m_sJudgeStr == "BLANK")
            //{
            //    m_vGetCameraCenterPosition = m_PlayerCameraBox.SetCameraCenterPosition();
            //    m_vCameraCenterPosition = new Vector3(playerpos.x, playerpos.y, m_fFixZ);
            //    //m_tCameraTransform.position = m_vCameraCenterPosition;
            //    m_tCameraTransform.position = Vector3.Lerp(m_tCameraTransform.position, m_vCameraCenterPosition, 0.025f * (Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_Speed() * 0.03f));
            //}
            //else
            //{
                m_vGetCameraCenterPosition = m_PlayerCameraBox.SetCameraCenterPosition();
                m_vCameraCenterPosition = new Vector3(m_vGetCameraCenterPosition.x, m_vGetCameraCenterPosition.y, m_fFixZ);
            //m_tCameraTransform.position = m_vCameraCenterPosition;
            m_tCameraTransform.position = Vector3.Lerp(m_tCameraTransform.position, m_vCameraCenterPosition, 0.025f * (Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_Speed() * 0.03f));
            //}
        }
        else if (m_PlayerCameraBox.m_eCC == PlayerCameraBox.E_CAMERA_CATEGORY.FULL)
        {
            m_tCameraTransform.position = m_vCameraCenterPosition;
        }
        else if (m_PlayerCameraBox.m_eCC == PlayerCameraBox.E_CAMERA_CATEGORY.ZOOMIN)
        {

        }

        return true;
    }

    public void SetCamera_NORMAL()
    {
        m_PlayerCameraBox.m_eCC = PlayerCameraBox.E_CAMERA_CATEGORY.NORMAL;
    }

    public void SetCamera_FULL(Vector3 pos)
    {
        m_PlayerCameraBox.m_eCC = PlayerCameraBox.E_CAMERA_CATEGORY.FULL;
        m_vCameraCenterPosition = pos;
    }

    PlayerCameraBox.E_CAMERA_CATEGORY m_CC_Before;
    public void SetCamera_ZOOMIN(Vector3 pos)
    {
        if (m_PlayerCameraBox.m_eCC != PlayerCameraBox.E_CAMERA_CATEGORY.ZOOMIN)
            m_CC_Before = m_PlayerCameraBox.m_eCC;

        m_PlayerCameraBox.m_eCC = PlayerCameraBox.E_CAMERA_CATEGORY.ZOOMIN;
        m_vCameraCenterPosition = pos;

        if (m_cProcess_ZoomIn != null)
        {
            StopCoroutine(m_cProcess_ZoomIn);
        }
        if (m_cProcess_ZoomOut != null)
        {
            StopCoroutine(m_cProcess_ZoomOut);
            m_cProcess_ZoomOut = null;
        }

        m_cProcess_ZoomIn = StartCoroutine(ProcessZoomIn(pos));
    }
    Coroutine m_cProcess_ZoomIn;
    IEnumerator ProcessZoomIn(Vector3 pos)
    {
        Player_Total.Instance.m_pm_Move.m_bMove = false;
        Player_Total.Instance.m_pm_Move.Move(0, 0, 0);
        float RateOfChange = 0.02f;
        //Vector2 m_vStartingPoint = m_PlayerCameraBox.GetCameraCenterPosition();
        Vector2 m_vStartingPoint = m_tCameraTransform.position;
        while (m_tCameraTransform.position.z < -1f)
        {
            Vector2 LerpVector = Vector2.Lerp(m_vStartingPoint, pos, 0.01f);
            m_tCameraTransform.position = new Vector3(LerpVector.x, LerpVector.y, m_tCameraTransform.position.z + RateOfChange);
            m_vStartingPoint = m_tCameraTransform.position;
            yield return new WaitForSeconds(0.01f);
        }
        if (m_cProcess_ZoomIn != null)
            m_cProcess_ZoomIn = null;
    }

    public void ZoomOut()
    {
        if (m_cProcess_ZoomIn != null)
        {
            StopCoroutine(m_cProcess_ZoomIn);
            m_cProcess_ZoomIn = null;
        }
        if (m_cProcess_ZoomOut != null)
            StopCoroutine(m_cProcess_ZoomOut);

        m_cProcess_ZoomOut = StartCoroutine(ProcessReleaseZoomOut(m_gCamera.transform.position));
    }
    Coroutine m_cProcess_ZoomOut;
    IEnumerator ProcessReleaseZoomOut(Vector3 pos)
    {
        float RateOfChange = 0.02f;
        Vector2 m_vStartingPoint = pos;
        while (m_tCameraTransform.position.z > -2.25f)
        {
            Vector2 LerpVector = Vector2.Lerp(m_vStartingPoint, m_PlayerCameraBox.GetCameraCenterPosition(), 0.01f);
            m_tCameraTransform.position = new Vector3(LerpVector.x, LerpVector.y, m_tCameraTransform.position.z - RateOfChange);
            m_vStartingPoint = m_tCameraTransform.position;
            yield return new WaitForSeconds(0.01f);
        }
        m_PlayerCameraBox.m_eCC = m_CC_Before;
        if (m_cProcess_ZoomOut != null)
            m_cProcess_ZoomOut = null;

        Player_Total.Instance.m_pm_Move.m_bMove = true;
    }
}
