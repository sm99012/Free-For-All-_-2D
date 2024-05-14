using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraBox : MonoBehaviour
{
    int m_nLayer1; // 카메라 박스의 충돌 레이어

    Vector3 m_vVerticalDistance = new Vector3(0, 1.575f, 0);//new Vector3(0, 1.05f, 0); // 카메라 박스 크기(세로) _ Gizmo 전용 변수
    float m_fVerticalDistance = 1.575f; // 카메라 박스 크기(세로)
    Vector3 m_vHoriontalDistance = new Vector3(2.825f, 0, 0);//new Vector3(1.375f, 0, 0); // 카메라 박스 크기(가로) _ Gizmo 전용 변수
    float m_fHorizontalDistance = 2.825f; // 카메라 박스 크기(가로)

    // 카메라 박스의 영역을 레이를 이용해 제어. 아래는 해당 변수
    public RaycastHit2D m_rHit_UP; // ↑ 방향 레이
    public bool m_bHit_UP = false; // ↑ 방향 레이 충돌 결과
    Vector2 m_vHitPos_UP = new Vector2(); // ↑ 방향 레이의 충돌 지점
    public RaycastHit2D m_rHit_DOWN; // ↓ 방향 레이
    public bool m_bHit_DOWN = false; // ↓ 방향 레이 충돌 결과
    Vector2 m_vHitPos_DOWN = new Vector2(); // ↓ 방향 레이의 충돌 지점
    public RaycastHit2D m_rHit_RIGHT; // → 방향 레이
    public bool m_bHit_RIGHT = false; // → 방향 레이 충돌 결과
    Vector2 m_vHitPos_RIGHT = new Vector2(); // → 방향 레이의 충돌 지점
    public RaycastHit2D m_rHit_LEFT; // ← 방향 레이
    public bool m_bHit_LEFT = false; // ← 방향 레이 충돌 결과
    Vector2 m_vHitPos_LEFT = new Vector2(); // ← 방향 레이의 충돌 지점

    public enum E_CAMERA_CATEGORY { NORMAL, FULL, ZOOMIN, SPECIAL} // 카메라 모드 FSM
    public E_CAMERA_CATEGORY m_eCC = E_CAMERA_CATEGORY.NORMAL;
    public enum E_CAMERA_POSITION { NULL, BLANK, UP, UP_RIGHT, RIGHT, RIGHT_DOWN, DOWN, DOWN_LEFT, LEFT, LEFT_UP} // 카메라 박스의 충돌 상태 FSM
    public E_CAMERA_POSITION m_eCP = E_CAMERA_POSITION.BLANK;

    // 카메라 박스의 충돌에 따라 카메라 박스의 중심점을 지정할때 사용되는 변수
    float m_fDistance_PG;
    float m_fDistance_Exceed;
    float m_fDistance_PG2;
    float m_fDistance_Exceed2;

    private void Awake()
    {
        m_nLayer1 = 1 << LayerMask.NameToLayer("CameraWall"); // 카메라 박스의 충돌 레이어 설정
    }

    // 카메라 모드가 일반 모드일때 8개 방향의 카메라 박스의 충돌 상태를 판단하는 함수. 이를 기반으로 카메라의 중심점을 지정한다.
    // 플레이어의 위치를 기준으로 상, 하, 좌, 우 방향의 레이를 이용.
    // ↖  ↑  ↗
    // ←       →
    // ↙  ↓  ↘
    // 8개 방향의 카메라 박스의 충돌 상태
    public string JudgeCameraCenterPosition()
    {
        if (m_eCC == E_CAMERA_CATEGORY.NORMAL)
        {
            m_rHit_UP = Physics2D.Raycast(this.transform.position, Vector2.up, m_fVerticalDistance, m_nLayer1);
            if (m_rHit_UP.collider != null)
            {
                m_bHit_UP = true;
                m_vHitPos_UP = m_rHit_UP.point;
            }
            else
                m_bHit_UP = false;

            m_rHit_DOWN = Physics2D.Raycast(this.transform.position, Vector2.down, m_fVerticalDistance, m_nLayer1);
            if (m_rHit_DOWN.collider != null)
            {
                m_bHit_DOWN = true;
                m_vHitPos_DOWN = m_rHit_DOWN.point;
            }
            else
                m_bHit_DOWN = false;

            m_rHit_RIGHT = Physics2D.Raycast(this.transform.position, Vector2.right, m_fHorizontalDistance, m_nLayer1);
            if (m_rHit_RIGHT.collider != null)
            {
                m_bHit_RIGHT = true;
                m_vHitPos_RIGHT = m_rHit_RIGHT.point;
            }
            else
                m_bHit_RIGHT = false;

            m_rHit_LEFT = Physics2D.Raycast(this.transform.position, Vector2.left, m_fHorizontalDistance, m_nLayer1);
            if (m_rHit_LEFT.collider != null)
            {
                m_bHit_LEFT = true;
                m_vHitPos_LEFT = m_rHit_LEFT.point;
            }
            else
                m_bHit_LEFT = false;

            if (m_bHit_UP == false && m_bHit_DOWN == false &&
                m_bHit_RIGHT == false && m_bHit_LEFT == false)
            {
                m_eCP = E_CAMERA_POSITION.BLANK;
                return "BLANK";
            }
            else if (m_bHit_UP == true && m_bHit_DOWN == false &&
                m_bHit_RIGHT == false && m_bHit_LEFT == false)
            {
                m_eCP = E_CAMERA_POSITION.UP;
                return "UP";
            }
            else if (m_bHit_UP == true && m_bHit_DOWN == false &&
                m_bHit_RIGHT == true && m_bHit_LEFT == false)
            {
                m_eCP = E_CAMERA_POSITION.UP_RIGHT;
                return "UP_RIGHT";
            }
            else if (m_bHit_UP == false && m_bHit_DOWN == true &&
                m_bHit_RIGHT == false && m_bHit_LEFT == false)
            {
                m_eCP = E_CAMERA_POSITION.DOWN;
                return "DOWN";
            }
            else if (m_bHit_UP == false && m_bHit_DOWN == true &&
                m_bHit_RIGHT == false && m_bHit_LEFT == true)
            {
                m_eCP = E_CAMERA_POSITION.DOWN_LEFT;
                return "DOWN_LEFT";
            }
            else if (m_bHit_UP == false && m_bHit_DOWN == false &&
                m_bHit_RIGHT == true && m_bHit_LEFT == false)
            {
                m_eCP = E_CAMERA_POSITION.RIGHT;
                return "RIGHT";
            }
            else if (m_bHit_UP == false && m_bHit_DOWN == true &&
                m_bHit_RIGHT == true && m_bHit_LEFT == false)
            {
                m_eCP = E_CAMERA_POSITION.RIGHT_DOWN;
                return "RIGHT_DOWN";
            }
            else if (m_bHit_UP == false && m_bHit_DOWN == false &&
                m_bHit_RIGHT == false && m_bHit_LEFT == true)
            {
                m_eCP = E_CAMERA_POSITION.LEFT;
                return "LEFT";
            }
            else if (m_bHit_UP == true && m_bHit_DOWN == false &&
                m_bHit_RIGHT == false && m_bHit_LEFT == true)
            {
                m_eCP = E_CAMERA_POSITION.LEFT_UP;
                return "LEFT_UP";
            }
            else
            {
                m_eCP = E_CAMERA_POSITION.NULL;
                return "NULL";
            }
        }

        return "NULL";
    }

    Vector3 m_vCameraCenterPosition; // 카메라 박스의 중심점
    public Vector3 GetCameraCenterPosition()
    {
        return m_vCameraCenterPosition;
    }
    public Vector3 SetCameraCenterPosition()
    {
        // 카메라 박스가 충돌하지 않을때 카메라 박스의 중심점은 플레이어의 위치와 동일하다.
        if (m_eCP == E_CAMERA_POSITION.BLANK)
        {
            m_vCameraCenterPosition = Player_Total.Instance.gameObject.transform.position;
        }
        // 카메라 박스가 ↑ 방향 충돌할때 카메라 박스의 중심점의 x값은 플레이어의 위치와 동일하지만 고정된 특정 y값을 가진다.
        // 고정된 특정 y값은 카메라 박스의 ↑ 방향 충돌 지점에서 부터 m_fVerticalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.UP)
        {
            m_fDistance_PG = m_vHitPos_UP.y - this.transform.position.y;
            m_fDistance_Exceed = m_fVerticalDistance - m_fDistance_PG;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x, this.transform.position.y - m_fDistance_Exceed);
        }
        // 카메라 박스가 ↗ 방향 충돌할때 카메라 박스의 중심점은 고정된 특정 x, y값을 가진다.
        // 고정된 특정 x값은 카메라 박스의 ↗ 방향 충돌 지점에서 부터 m_fHorizontalDistance 만큼의 절대값을 가진다.
        // 고정된 특정 y값은 카메라 박스의 ↗ 방향 충돌 지점에서 부터 m_fVerticalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.UP_RIGHT)
        {
            m_fDistance_PG = m_vHitPos_UP.y - this.transform.position.y;
            m_fDistance_Exceed = m_fVerticalDistance - m_fDistance_PG;
            m_fDistance_PG2 = m_vHitPos_RIGHT.x - this.transform.position.x;
            m_fDistance_Exceed2 = m_fHorizontalDistance - m_fDistance_PG2;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x - m_fDistance_Exceed2, this.transform.position.y - m_fDistance_Exceed);
        }
        // 카메라 박스가 ↓ 방향 충돌할때 카메라 박스의 중심점의 x값은 플레이어의 위치와 동일하지만 고정된 특정 y값을 가진다.
        // 고정된 특정 y값은 카메라 박스의 ↓ 방향 충돌 지점에서 부터 m_fVerticalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.DOWN)
        {
            m_fDistance_PG = this.transform.position.y - m_vHitPos_DOWN.y;
            m_fDistance_Exceed = m_fVerticalDistance - m_fDistance_PG;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x, this.transform.position.y + m_fDistance_Exceed);
        }
        // 카메라 박스가 ↙ 방향 충돌할때 카메라 박스의 중심점은 고정된 특정 x, y값을 가진다.
        // 고정된 특정 x값은 카메라 박스의 ↙ 방향 충돌 지점에서 부터 m_fHorizontalDistance 만큼의 절대값을 가진다.
        // 고정된 특정 y값은 카메라 박스의 ↙ 방향 충돌 지점에서 부터 m_fVerticalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.DOWN_LEFT)
        {
            m_fDistance_PG = this.transform.position.y - m_vHitPos_DOWN.y;
            m_fDistance_Exceed = m_fVerticalDistance - m_fDistance_PG;
            m_fDistance_PG2 = this.transform.position.x - m_vHitPos_LEFT.x;
            m_fDistance_Exceed2 = m_fHorizontalDistance - m_fDistance_PG2;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x + m_fDistance_Exceed2, this.transform.position.y + m_fDistance_Exceed);
        }
        // 카메라 박스가 → 방향 충돌할때 카메라 박스의 중심점의 y값은 플레이어의 위치와 동일하지만 고정된 특정 x값을 가진다.
        // 고정된 특정 x값은 카메라 박스의 → 방향 충돌 지점에서 부터 m_fHorizontalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.RIGHT)
        {
            m_fDistance_PG = m_vHitPos_RIGHT.x - this.transform.position.x;
            m_fDistance_Exceed = m_fHorizontalDistance - m_fDistance_PG;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x - m_fDistance_Exceed, this.transform.position.y);
        }
        // 카메라 박스가 ↘ 방향 충돌할때 카메라 박스의 중심점은 고정된 특정 x, y값을 가진다.
        // 고정된 특정 x값은 카메라 박스의 ↘ 방향 충돌 지점에서 부터 m_fHorizontalDistance 만큼의 절대값을 가진다.
        // 고정된 특정 y값은 카메라 박스의 ↘ 방향 충돌 지점에서 부터 m_fVerticalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.RIGHT_DOWN)
        {
            m_fDistance_PG = this.transform.position.y - m_vHitPos_DOWN.y;
            m_fDistance_Exceed = m_fVerticalDistance - m_fDistance_PG;
            m_fDistance_PG2 = m_vHitPos_RIGHT.x - this.transform.position.x;
            m_fDistance_Exceed2 = m_fHorizontalDistance - m_fDistance_PG2;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x - m_fDistance_Exceed2, this.transform.position.y + m_fDistance_Exceed);
        }
        // 카메라 박스가 ← 방향 충돌할때 카메라 박스의 중심점의 y값은 플레이어의 위치와 동일하지만 고정된 특정 x값을 가진다.
        // 고정된 특정 x값은 카메라 박스의 ← 방향 충돌 지점에서 부터 m_fHorizontalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.LEFT)
        {
            m_fDistance_PG = this.transform.position.x - m_vHitPos_LEFT.x;
            m_fDistance_Exceed = m_fHorizontalDistance - m_fDistance_PG;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x + m_fDistance_Exceed, this.transform.position.y);
        }
        // 카메라 박스가 ↖ 방향 충돌할때 카메라 박스의 중심점은 고정된 특정 x, y값을 가진다.
        // 고정된 특정 x값은 카메라 박스의 ↖ 방향 충돌 지점에서 부터 m_fHorizontalDistance 만큼의 절대값을 가진다.
        // 고정된 특정 y값은 카메라 박스의 ↖ 방향 충돌 지점에서 부터 m_fVerticalDistance 만큼의 절대값을 가진다.
        else if (m_eCP == E_CAMERA_POSITION.LEFT_UP)
        {
            m_fDistance_PG = m_vHitPos_UP.y - this.transform.position.y;
            m_fDistance_Exceed = m_fVerticalDistance - m_fDistance_PG;
            m_fDistance_PG2 = this.transform.position.x - m_vHitPos_LEFT.x;
            m_fDistance_Exceed2 = m_fHorizontalDistance - m_fDistance_PG2;
            m_vCameraCenterPosition = new Vector2(this.transform.position.x + m_fDistance_Exceed2, this.transform.position.y - m_fDistance_Exceed);
        }

        return m_vCameraCenterPosition; // 카메라 박스의 중심점을 반환
    }    

    // 기즈모 관련 함수. 카메라 박스의 중심점과 충돌 여부를 확인한다.
    private void OnDrawGizmos()
    {
        if (m_bHit_UP == false)
            Gizmos.color = Color.white;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + m_vVerticalDistance);

        if (m_bHit_DOWN == false)
            Gizmos.color = Color.white;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position - m_vVerticalDistance);

        if (m_bHit_RIGHT == false)
            Gizmos.color = Color.white;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + m_vHoriontalDistance);

        if (m_bHit_LEFT == false)
            Gizmos.color = Color.white;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position - m_vHoriontalDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(m_vCameraCenterPosition, 0.3f);
    }
}
