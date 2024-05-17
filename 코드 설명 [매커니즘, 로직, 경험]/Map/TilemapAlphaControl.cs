using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAlphaControl : MonoBehaviour
{
    [SerializeField]
    Tilemap m_Tilemap_TreeUpper;
    [SerializeField]
    Tilemap m_Tilemap_TreeLower;

    [SerializeField]
    Vector3Int m_v3PlayerPos;
    [SerializeField]
    Vector3Int m_v3DetectingArea;

    Tile m_Tile_Tree1_1_UpperAlpha100;
    Tile m_Tile_Tree1_1_UpperAlpha50;
    Tile m_Tile_Tree1_2_UpperAlpha100;
    Tile m_Tile_Tree1_2_UpperAlpha50;
    Tile m_Tile_Tree1_3_UpperAlpha100;
    Tile m_Tile_Tree1_3_UpperAlpha50;
    Tile m_Tile_Tree1_4_UpperAlpha100;
    Tile m_Tile_Tree1_4_UpperAlpha50;
    Tile m_Tile_Tree1_LowerAlpha100;
    Tile m_Tile_Tree1_LowerAlpha50;
    
    Tile m_Tile_Tree2_1_UpperAlpha100;
    Tile m_Tile_Tree2_1_UpperAlpha50;
    Tile m_Tile_Tree2_2_UpperAlpha100;
    Tile m_Tile_Tree2_2_UpperAlpha50;
    Tile m_Tile_Tree2_3_UpperAlpha100;
    Tile m_Tile_Tree2_3_UpperAlpha50;
    Tile m_Tile_Tree2_4_UpperAlpha100;
    Tile m_Tile_Tree2_4_UpperAlpha50;
    Tile m_Tile_Tree2_LowerAlpha100;
    Tile m_Tile_Tree2_LowerAlpha50;

    Tile m_Tile_Tree3_1_UpperAlpha100;
    Tile m_Tile_Tree3_1_UpperAlpha50;
    Tile m_Tile_Tree3_2_UpperAlpha100;
    Tile m_Tile_Tree3_2_UpperAlpha50;
    Tile m_Tile_Tree3_3_UpperAlpha100;
    Tile m_Tile_Tree3_3_UpperAlpha50;
    Tile m_Tile_Tree3_4_UpperAlpha100;
    Tile m_Tile_Tree3_4_UpperAlpha50;
    Tile m_Tile_Tree3_LowerAlpha100;
    Tile m_Tile_Tree3_LowerAlpha50;

    // Player 탐지 범위.(Tilemap 이 Player 를 인식하고 Color 의 Alpha 값을 조절하는데 관련.)
    int m_nDetectingArea;
    // Tilemap 의 Tile 을 다시 불투명하게 전환 하는 범위.
    int m_nRecoverintArea;

    // Start is called before the first frame update
    void Start()
    {
        InitialSet();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTile();
    }

    void InitialSet()
    {
        m_nDetectingArea = 3;
        m_nRecoverintArea = 2;

        m_Tile_Tree1_1_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_1_Upper Alpha100");
        m_Tile_Tree1_1_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_1_Upper Alpha50");
        m_Tile_Tree1_2_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_2_Upper Alpha100");
        m_Tile_Tree1_2_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_2_Upper Alpha50");
        m_Tile_Tree1_3_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_3_Upper Alpha100");
        m_Tile_Tree1_3_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_3_Upper Alpha50");
        m_Tile_Tree1_4_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_4_Upper Alpha100");
        m_Tile_Tree1_4_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_4_Upper Alpha50");
        m_Tile_Tree1_LowerAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_Lower Alpha100");
        m_Tile_Tree1_LowerAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree1/Tree1_Lower Alpha50");

        m_Tile_Tree2_1_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_1_Upper Alpha100");
        m_Tile_Tree2_1_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_1_Upper Alpha50");
        m_Tile_Tree2_2_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_2_Upper Alpha100");
        m_Tile_Tree2_2_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_2_Upper Alpha50");
        m_Tile_Tree2_3_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_3_Upper Alpha100");
        m_Tile_Tree2_3_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_3_Upper Alpha50");
        m_Tile_Tree2_4_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_4_Upper Alpha100");
        m_Tile_Tree2_4_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_4_Upper Alpha50");
        m_Tile_Tree2_LowerAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_Lower Alpha100");
        m_Tile_Tree2_LowerAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree2/Tree2_Lower Alpha50");

        m_Tile_Tree3_1_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_1_Upper Alpha100");
        m_Tile_Tree3_1_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_1_Upper Alpha50");
        m_Tile_Tree3_2_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_2_Upper Alpha100");
        m_Tile_Tree3_2_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_2_Upper Alpha50");
        m_Tile_Tree3_3_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_3_Upper Alpha100");
        m_Tile_Tree3_3_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_3_Upper Alpha50");
        m_Tile_Tree3_4_UpperAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_4_Upper Alpha100");
        m_Tile_Tree3_4_UpperAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_4_Upper Alpha50");
        m_Tile_Tree3_LowerAlpha100 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_Lower Alpha100");
        m_Tile_Tree3_LowerAlpha50 = Resources.Load<Tile>("Prefab/Tile/Tree/Tree3/Tree3_Lower Alpha50");
    }

    void CheckTile()
    {
        m_v3PlayerPos = m_Tilemap_TreeUpper.WorldToCell(Player_Total.Instance.gameObject.transform.position);
        CheckTile_Alpha();
        //Debug.Log("Tilemap_PlayerPos: " + m_v3PlayerPos);
    }

    int m_nCheckTile_Alpha_y;
    int m_nCheckTile_Alpha_x;
    int m_nDetectingArea_y;
    int m_nDetectingArea_x;
    void CheckTile_Alpha()
    {
        for (m_nCheckTile_Alpha_y = m_v3PlayerPos.y - (m_nDetectingArea + m_nRecoverintArea), m_nDetectingArea_y = 0; m_nDetectingArea_y < ((m_nDetectingArea + m_nRecoverintArea) * 2) + 1; m_nDetectingArea_y++, m_nCheckTile_Alpha_y++)
        {
            for (m_nCheckTile_Alpha_x = m_v3PlayerPos.x - (m_nDetectingArea + m_nRecoverintArea), m_nDetectingArea_x = 0; m_nDetectingArea_x < ((m_nDetectingArea + m_nRecoverintArea) * 2) + 1; m_nDetectingArea_x++, m_nCheckTile_Alpha_x++)
            {
                m_v3DetectingArea = new Vector3Int(m_nCheckTile_Alpha_x, m_nCheckTile_Alpha_y, 0);

                if (m_nDetectingArea_y > m_nRecoverintArea - 1 && m_nDetectingArea_y < (((m_nDetectingArea + m_nRecoverintArea) * 2) - m_nRecoverintArea) &&
                    m_nDetectingArea_x > m_nRecoverintArea && m_nDetectingArea_x < (((m_nDetectingArea + m_nRecoverintArea) * 2) - m_nRecoverintArea ))
                {
                    if (m_Tilemap_TreeUpper.HasTile(m_v3DetectingArea))
                    {
                        //Debug.Log("[" + m_nCheckTile_Alpha_x + ", " + m_nCheckTile_Alpha_y + "]: " + m_Tilemap_TreeUpper.GetTile(m_v3DetectingArea).name);

                        CheckTile_Tilemap_TreeUpper_Alpha_SwitchFunction_ToAlpha50(m_v3DetectingArea, m_Tilemap_TreeUpper.GetTile(m_v3DetectingArea).name);
                    }
                    if (m_Tilemap_TreeLower.HasTile(m_v3DetectingArea))
                    {
                        //Debug.Log("[" + m_nCheckTile_Alpha_x + ", " + m_nCheckTile_Alpha_y + "]: " + m_Tilemap_TreeUpper.GetTile(m_v3DetectingArea).name);

                        CheckTile_Tilemap_TreeLower_Alpha_SwitchFunction_ToAlpha50(m_v3DetectingArea, m_Tilemap_TreeLower.GetTile(m_v3DetectingArea).name);
                    }
                }
                else
                {
                    if (m_Tilemap_TreeUpper.HasTile(m_v3DetectingArea))
                    {
                        //Debug.Log("[" + m_nCheckTile_Alpha_x + ", " + m_nCheckTile_Alpha_y + "]: " + m_Tilemap_TreeUpper.GetTile(m_v3DetectingArea).name);

                        CheckTile_Tilemap_TreeUpper_Alpha_SwitchFunction_ToAlpha100(m_v3DetectingArea, m_Tilemap_TreeUpper.GetTile(m_v3DetectingArea).name);
                    }
                    if (m_Tilemap_TreeLower.HasTile(m_v3DetectingArea))
                    {
                        //Debug.Log("[" + m_nCheckTile_Alpha_x + ", " + m_nCheckTile_Alpha_y + "]: " + m_Tilemap_TreeUpper.GetTile(m_v3DetectingArea).name);

                        CheckTile_Tilemap_TreeLower_Alpha_SwitchFunction_ToAlpha100(m_v3DetectingArea, m_Tilemap_TreeLower.GetTile(m_v3DetectingArea).name);
                    }
                }
            }
        }
    }
    void CheckTile_Tilemap_TreeUpper_Alpha_SwitchFunction_ToAlpha50(Vector3Int v3, string str)
    {
        switch (str)
        {
            case "Tree1_1_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_1_UpperAlpha50);
                }
                break;
            case "Tree1_2_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_2_UpperAlpha50);
                }
                break;
            case "Tree1_3_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_3_UpperAlpha50);
                }
                break;
            case "Tree1_4_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_4_UpperAlpha50);
                }
                break;
            case "Tree2_1_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_1_UpperAlpha50);
                }
                break;
            case "Tree2_2_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_2_UpperAlpha50);
                }
                break;
            case "Tree2_3_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_3_UpperAlpha50);
                }
                break;
            case "Tree2_4_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_4_UpperAlpha50);
                }
                break;
            case "Tree3_1_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_1_UpperAlpha50);
                }
                break;
            case "Tree3_2_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_2_UpperAlpha50);
                }
                break;
            case "Tree3_3_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_3_UpperAlpha50);
                }
                break;
            case "Tree3_4_Upper Alpha100":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_4_UpperAlpha50);
                }
                break;
        }
    }
    void CheckTile_Tilemap_TreeLower_Alpha_SwitchFunction_ToAlpha50(Vector3Int v3, string str)
    {
        switch (str)
        {
            case "Tree1_Lower Alpha100":
                {
                    m_Tilemap_TreeLower.SetTile(m_v3DetectingArea, m_Tile_Tree1_LowerAlpha50);
                }
                break;
            case "Tree2_Lower Alpha100":
                {
                    m_Tilemap_TreeLower.SetTile(m_v3DetectingArea, m_Tile_Tree2_LowerAlpha50);
                }
                break;
            case "Tree3_Lower Alpha100":
                {
                    m_Tilemap_TreeLower.SetTile(m_v3DetectingArea, m_Tile_Tree3_LowerAlpha50);
                }
                break;
        }
    }
    void CheckTile_Tilemap_TreeUpper_Alpha_SwitchFunction_ToAlpha100(Vector3Int v3, string str)
    {
        switch (str)
        {
            case "Tree1_1_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_1_UpperAlpha100);
                }
                break;
            case "Tree1_2_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_2_UpperAlpha100);
                }
                break;
            case "Tree1_3_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_3_UpperAlpha100);
                }
                break;
            case "Tree1_4_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree1_4_UpperAlpha100);
                }
                break;
            case "Tree2_1_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_1_UpperAlpha100);
                }
                break;
            case "Tree2_2_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_2_UpperAlpha100);
                }
                break;
            case "Tree2_3_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_3_UpperAlpha100);
                }
                break;
            case "Tree2_4_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree2_4_UpperAlpha100);
                }
                break;
            case "Tree3_1_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_1_UpperAlpha100);
                }
                break;
            case "Tree3_2_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_2_UpperAlpha100);
                }
                break;
            case "Tree3_3_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_3_UpperAlpha100);
                }
                break;
            case "Tree3_4_Upper Alpha50":
                {
                    m_Tilemap_TreeUpper.SetTile(m_v3DetectingArea, m_Tile_Tree3_4_UpperAlpha100);
                }
                break;
        }
    }
    void CheckTile_Tilemap_TreeLower_Alpha_SwitchFunction_ToAlpha100(Vector3Int v3, string str)
    {
        switch (str)
        {
            case "Tree1_Lower Alpha50":
                {
                    m_Tilemap_TreeLower.SetTile(m_v3DetectingArea, m_Tile_Tree1_LowerAlpha100);
                }
                break;
            case "Tree2_Lower Alpha50":
                {
                    m_Tilemap_TreeLower.SetTile(m_v3DetectingArea, m_Tile_Tree2_LowerAlpha100);
                }
                break;
            case "Tree3_Lower Alpha50":
                {
                    m_Tilemap_TreeLower.SetTile(m_v3DetectingArea, m_Tile_Tree3_LowerAlpha100);
                }
                break;
        }
    }


}
