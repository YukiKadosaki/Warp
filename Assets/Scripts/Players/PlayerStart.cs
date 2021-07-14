using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    private int m_PSNumber;//プレイヤースタートの識別子
    private bool m_Active;

    public int PSNumber
    {
        get => m_PSNumber;
        set { m_PSNumber = value; }
    }

    public bool Active
    {
        get => m_Active;
        set { m_Active = value; }
    }


    public void CreatePlayer()
    {
        if (Active)
        {
            Instantiate((GameObject)Resources.Load("Prefab/Player"), this.transform.position, Quaternion.identity) ;
        }
    }
}
