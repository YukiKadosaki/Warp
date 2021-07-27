using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    private int m_PSNumber;//プレイヤースタートの識別子
    private bool m_Active = false;
    private bool m_Touched = false;//触れられたことがあるかどうか（セーブ音に使う）
    private SpriteRenderer m_SpriteRenderer;

    public int PSNumber
    {
        get => m_PSNumber;
        set { m_PSNumber = value; }
    }

    //NextMapTriggerから呼ばれる
    public bool Active
    {
        get => m_Active;
        set { m_Active = value; }
    }
    public bool Touched
    {
        get => m_Touched;
        set { m_Touched = value; }
    }

    private void Awake()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        //m_SpriteRenderer.enabled = false;
    }


    //Player_Sumpleと、NextMapTriggerから呼ばれる
    public IEnumerator CreatePlayer()
    {
        Debug.Log("Called");
        if (Active)
        {
            Vector3 pos = this.transform.position;
            pos.z = 0;
            GameObject obj = Instantiate((GameObject)Resources.Load("Prefab/Player"), pos, Quaternion.identity);
            if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                Destroy(obj);
            }
        }
        yield break;
    }

    //Player_SampleからCreatePlayerを呼び出すため
    public void CreatePlayerVoid()
    {
        if (Active)
        {
            Vector3 pos = this.transform.position;
            pos.z = 0;
            GameObject obj = Instantiate((GameObject)Resources.Load("Prefab/Player"), pos, Quaternion.identity);
            if (GameObject.FindGameObjectsWithTag("Player").Length > 2)
            {
                Destroy(obj);
            }
        }
    }

    public void ChangeSprite(Sprite s)
    {
        m_SpriteRenderer.sprite = s;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //セーブSE
        Player_Sample p;
        if(!Touched && collision.TryGetComponent<Player_Sample>(out p))
        {
            Touched = true;
            p.SaveSE();
        }
    }
}
