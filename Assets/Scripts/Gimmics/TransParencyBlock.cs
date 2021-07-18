using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransParencyBlock : MonoBehaviour
{
    private enum State
    {
        transparent,
        nonTransparent
    }

    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    private State m_State;
    
    private Sprite m_Sprite;
    private Collider2D m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_Sprite= Resources.Load<Sprite>("Sprites/Transparent");
        m_SpriteRenderer.sprite = m_Sprite; //�G�f�B�^��ʂł͈Ⴄ�X�v���C�g�ɂ��邽��
        m_Collider = this.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //��������������Ȃ�����ύX����
            if (m_State == State.nonTransparent)
            {
                m_State = State.transparent;
            }
            else
            {
                m_State = State.nonTransparent;
            }
        }

        //���̉����Ă���Ƃ�
        if (m_State == State.nonTransparent)
        {
            m_SpriteRenderer.color = new Color(1, 1, 1, 1);
            m_Collider.isTrigger = false;
        }
        else//���������Ă���Ƃ�
        {
            m_SpriteRenderer.color = new Color(1, 1, 1, 0.3f);
            m_Collider.isTrigger = true;
        }
    }

    //�ׂ��ꂽ�Ƃ��̎��S����
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player_Sample p;
        if(Input.GetKeyDown(KeyCode.Z) && collision.TryGetComponent(out p))
        {
            Debug.Log("KILL");
            StartCoroutine(p.KillPlayer());
        }
    }



}
