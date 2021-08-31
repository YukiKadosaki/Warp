using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    private int m_PSNumber;//�v���C���[�X�^�[�g�̎��ʎq
    private bool m_Active = false;
    private bool m_Touched = false;//�G���ꂽ���Ƃ����邩�ǂ����i�Z�[�u���Ɏg���j
    private SpriteRenderer m_SpriteRenderer;
    private TowerManager m_TowerManager;

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

    private void Start()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_TowerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
    }


    //Player_Sample����ĂԂƂ��́A�v���C���[�������Ă���ĂԕK�v������̂ň�U�R���[�`�����Ă�
    public IEnumerator CreatePlayer()
    {
        yield return null;
        CreatePlayerVoid();
    }

    //Player_Sample����CreatePlayer���Ăяo������
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
        //�Z�[�uSE
        Player_Sample p;
        Sprite s;
        if(!Touched && collision.TryGetComponent<Player_Sample>(out p))
        {
            Touched = true;
            p.SaveSE();
            s = Resources.Load<Sprite>("Sprites/aftersave");
            ChangeSprite(s);

            //�f�o�b�O�p�@��ŏ���
            GameObject obj = GameObject.FindGameObjectWithTag("Debug");
            obj.GetComponent<Text>().text = "PlayerStartTouched";
            for (int i = 0; i < UnityEngine.Random.Range(0, 10); i++)
            {
                obj.GetComponent<Text>().text += "!";
            }

            //�K�w��ۑ�����
            m_TowerManager.SaveData.floorNumber = this.PSNumber;
        }
        

    }
}
