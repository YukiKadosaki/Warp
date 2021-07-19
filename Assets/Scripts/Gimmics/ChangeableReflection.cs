using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableReflection : Reflection
{
    protected Sprite[] m_Sprites = new Sprite[4];//向きは4つ
    private SpriteRenderer m_SpriteRenderer;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        LoadSprite();
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    //向きの変更
    public void ChangeMyDirection()
    {
        //反射タイプを変更する
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

        //反射タイプ通りのスプライトに変更する
        m_SpriteRenderer.sprite = m_Sprites[(int)m_ReflectType];
    }

    //スプライトのロード
    protected void LoadSprite()
    {
        m_Sprites[(int)ReflectType.rightUp] = Resources.Load<Sprite>("Sprites/hansha1");
        m_Sprites[(int)ReflectType.rightDown] = Resources.Load<Sprite>("Sprites/hansha2");
        m_Sprites[(int)ReflectType.leftDown] = Resources.Load<Sprite>("Sprites/hansha3");
        m_Sprites[(int)ReflectType.leftUp] = Resources.Load<Sprite>("Sprites/hansha4");
    }
}
