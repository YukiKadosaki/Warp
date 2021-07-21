/*分身クラス*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy : MonoBehaviour
{
    private const float copySpeed = 5;//分身の移動速度

    private Player_Sample m_Player;
    private Transform m_Transform;
    private Rigidbody2D m_RigidBody2D;
    private AudioSource m_AudioSource;


    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Sample>();
        m_Transform = this.transform;
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_AudioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && !collision.isTrigger){
            //プレイヤーをワープさせる
            m_Player.WarpToCopy(m_Transform.position);
            Destroy(this.gameObject);
        }
    }

    //反射オブジェクトに触れたときの処理
    public void ChangeDirection(Reflection.ReflectType rtype)
    {
        //音を鳴らす
        m_AudioSource.clip = Resources.Load<AudioClip>("Audio/SE/DirChange");
        m_AudioSource.Play();

        Vector3 v = m_RigidBody2D.velocity;//現在の速度

        //向きが右上
        if (rtype == Reflection.ReflectType.rightUp)
        {
            if(v.x < 0)
            {
                m_RigidBody2D.velocity = Vector3.up * copySpeed;
            }
            else if(v.y < 0)
            {
                m_RigidBody2D.velocity = Vector3.right * copySpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if(rtype == Reflection.ReflectType.rightDown)
        {
            if (v.x < 0)
            {
                m_RigidBody2D.velocity = Vector3.down * copySpeed;
            }
            else if (v.y > 0)
            {
                m_RigidBody2D.velocity = Vector3.right * copySpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if(rtype == Reflection.ReflectType.leftDown)
        {
            if (v.x > 0)
            {
                m_RigidBody2D.velocity = Vector3.down * copySpeed;
            }
            else if (v.y > 0)
            {
                m_RigidBody2D.velocity = Vector3.left * copySpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else if(rtype == Reflection.ReflectType.leftUp)
        {
            if (v.x > 0)
            {
                m_RigidBody2D.velocity = Vector3.up * copySpeed;
            }
            else if (v.y < 0)
            {
                m_RigidBody2D.velocity = Vector3.left * copySpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
