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
    protected ReflectType m_StartReflectType;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_transform = transform;
        m_StartReflectType = m_ReflectType;
    }

    //コピーが触れてきたら自分の向きを渡して方向を変えてもらう
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

    public void ReturnType()
    {
        m_ReflectType = m_StartReflectType;
    }
}
