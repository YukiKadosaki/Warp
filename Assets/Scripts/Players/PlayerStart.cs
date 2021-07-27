using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    private int m_PSNumber;//�v���C���[�X�^�[�g�̎��ʎq
    private bool m_Active = false;
    private bool m_Touched = false;//�G���ꂽ���Ƃ����邩�ǂ����i�Z�[�u���Ɏg���j
    private SpriteRenderer m_SpriteRenderer;

    public int PSNumber
    {
        get => m_PSNumber;
        set { m_PSNumber = value; }
    }

    //NextMapTrigger����Ă΂��
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


    //Player_Sumple�ƁANextMapTrigger����Ă΂��
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

    //Player_Sample����CreatePlayer���Ăяo������
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
        //�Z�[�uSE
        Player_Sample p;
        if(!Touched && collision.TryGetComponent<Player_Sample>(out p))
        {
            Touched = true;
            p.SaveSE();
        }
    }
}
