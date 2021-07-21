/*���g�N���X*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy : MonoBehaviour
{
    private const float copySpeed = 5;//���g�̈ړ����x

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
            //�v���C���[�����[�v������
            m_Player.WarpToCopy(m_Transform.position);
            Destroy(this.gameObject);
        }
    }

    //���˃I�u�W�F�N�g�ɐG�ꂽ�Ƃ��̏���
    public void ChangeDirection(Reflection.ReflectType rtype)
    {
        //����炷
        m_AudioSource.clip = Resources.Load<AudioClip>("Audio/SE/DirChange");
        m_AudioSource.Play();

        Vector3 v = m_RigidBody2D.velocity;//���݂̑��x

        //�������E��
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
