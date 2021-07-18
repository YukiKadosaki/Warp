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
    protected Transform m_transform;

    [SerializeField]
    protected ReflectType m_ReflectType;


    // Start is called before the first frame update
    protected virtual void Start()
    {
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

    
}
