using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMapTrigger : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Destination;
    [SerializeField]
    private int m_PSNumber;
    private MoveCamera m_Camera;
    private PlayerStart[] m_PS;

    public Vector3 Destination
    {
        get => m_Destination;
        set { m_Destination = value; }
    }

    public int PSNumber
    {
        get => m_PSNumber;
        set { m_PSNumber = value; }
    }
    public PlayerStart[] PS
    {
        get => m_PS;
        set { m_PS = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>();

        //PlayerStartを取得
        int i = 0;
        PS = new PlayerStart[GameObject.FindGameObjectsWithTag("PlayerStart").Length];
        foreach (GameObject ps in GameObject.FindGameObjectsWithTag("PlayerStart"))
        {
            PS[i] = ps.GetComponent<PlayerStart>();
            i++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //カメラを動かす
            StartCoroutine(m_Camera.MoveToDestination(Destination));

            //全PlayerStartのアクティブを切る
            for (int i = 0; i < PS.Length; i++)
            {
                PS[i].Active = false;
            }

            //PlayerStartをアクティベートする
            for (int i = 0;i < PS.Length; i++)
            {
                if(this.PSNumber == PS[i].PSNumber)
                {
                    PS[i].Active = true;
                }
            }
        }

    }
}
