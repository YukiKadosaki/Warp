using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    private int m_PSNumber;//プレイヤースタートの識別子
    private bool m_Active = false;
    private bool m_Touched = false;//触れられたことがあるかどうか（セーブ音に使う）
    private SpriteRenderer m_SpriteRenderer;
    private TowerManager m_TowerManager;

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

    private void Start()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_TowerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
    }


    //Player_Sampleから呼ぶときは、プレイヤーが消えてから呼ぶ必要があるので一旦コルーチンを呼ぶ
    public IEnumerator CreatePlayer()
    {
        yield return null;
        CreatePlayerVoid();
    }

    //Player_SampleからCreatePlayerを呼び出すため
    public void CreatePlayerVoid()
    {
        Debug.Log("CreatePlayerVoid");

        Vector3 pos = this.transform.position;
        pos.z = 0;
        GameObject obj = Instantiate((GameObject)Resources.Load("Prefab/Player"), pos, Quaternion.identity);
        if (GameObject.FindGameObjectsWithTag("Player").Length > 2)
        {
            Destroy(obj);
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
        Sprite s;
        if(!Touched && collision.TryGetComponent<Player_Sample>(out p))
        {
            Touched = true;
            p.SaveSE();
            s = Resources.Load<Sprite>("Sprites/aftersave");
            ChangeSprite(s);

            //デバッグ用　後で消す
            GameObject obj = GameObject.FindGameObjectWithTag("Debug");
            obj.GetComponent<Text>().text = "PlayerStartTouched";
            for (int i = 0; i < UnityEngine.Random.Range(0, 10); i++)
            {
                obj.GetComponent<Text>().text += "!";
            }

            //階層を保存する
            m_TowerManager.SaveData.floorNumber = this.PSNumber;
        }
        

    }
}
