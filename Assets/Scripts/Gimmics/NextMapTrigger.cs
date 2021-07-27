//PlayerPrefsには帰ってくるPSNumberを保存する

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
    private World m_World;

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
    void Awake()
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

        m_World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        //int activate_num = m_World.psNumber;
        int activate_num = 0;


        //PlayerStartをアクティベートする
        for (int j = 0; j < PS.Length; j++)
        {
            PS[activate_num].Active = true;
            StartCoroutine(PS[activate_num].CreatePlayer());
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
                Sprite s = Resources.Load<Sprite>("Sprites/beforesave");
                Debug.Log(s);
                PS[i].ChangeSprite(s);
            }

            //PlayerStartをアクティベートする
            for (int i = 0;i < PS.Length; i++)
            {
                if(this.PSNumber == PS[i].PSNumber)
                {
                    /*PlayerPrefs.SetInt("ReturnPSNum", this.PSNumber);
                    PlayerPrefs.Save();*/

                    PS[i].Active = true;
                    Sprite s = Resources.Load<Sprite>("Sprites/aftersave");
                    PS[i].ChangeSprite(s);
                }
            }
        }

    }
}
