using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    public enum ReflectType
    {
        rightUp,
        rightDown,
        leftDown,
        leftUp
    }
    private Transform m_transform;

    [SerializeField]
    private ReflectType m_ReflectType;

    Sprite[] m_Sprites = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite();
        m_transform = transform;
    }

    //�R�s�[���G��Ă����玩���̌�����n���ĕ�����ς��Ă��炤
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Copy copy;
        if(collision.TryGetComponent(out copy)){
            copy.transform.position = m_transform.position;
            copy.ChangeDirection(m_ReflectType);
            Debug.Log("VChange");
        }
        Debug.Log("Touch");
    }

    //�X�v���C�g�̃��[�h�q�N���X�ŕύX����
    protected void LoadSprite()
    {
        m_Sprites[(int)ReflectType.rightUp] = Resources.Load<Sprite>("Sprites/hansha1");
        m_Sprites[(int)ReflectType.rightDown] = Resources.Load<Sprite>("Sprites/hansha2");
        m_Sprites[(int)ReflectType.leftDown] = Resources.Load<Sprite>("Sprites/hansha3");
        m_Sprites[(int)ReflectType.leftUp] = Resources.Load<Sprite>("Sprites/hansha4");
    }
}
