using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableReflection : Reflection
{
    protected Sprite[] m_Sprites = new Sprite[4];//������4��
    private SpriteRenderer m_SpriteRenderer;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        LoadSprite();
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    //�����̕ύX
    public void ChangeMyDirection()
    {
        //���˃^�C�v��ύX����
        if(m_ReflectType == ReflectType.leftDown)
        {
            m_ReflectType = ReflectType.leftUp;
        }
        else if(m_ReflectType == ReflectType.leftUp)
        {
            m_ReflectType = ReflectType.rightUp;
        }
        else if (m_ReflectType == ReflectType.rightDown)
        {
            m_ReflectType = ReflectType.leftDown;
        }
        else if (m_ReflectType == ReflectType.rightUp)
        {
            m_ReflectType = ReflectType.rightDown;
        }

        //���˃^�C�v�ʂ�̃X�v���C�g�ɕύX����
        m_SpriteRenderer.sprite = m_Sprites[(int)m_ReflectType];
    }

    //�X�v���C�g�̃��[�h
    protected void LoadSprite()
    {
        m_Sprites[(int)ReflectType.rightUp] = Resources.Load<Sprite>("Sprites/hansha1");
        m_Sprites[(int)ReflectType.rightDown] = Resources.Load<Sprite>("Sprites/hansha2");
        m_Sprites[(int)ReflectType.leftDown] = Resources.Load<Sprite>("Sprites/hansha3");
        m_Sprites[(int)ReflectType.leftUp] = Resources.Load<Sprite>("Sprites/hansha4");
    }
}
