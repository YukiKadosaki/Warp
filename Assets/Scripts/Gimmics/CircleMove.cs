using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    [Header("���S�Ƃ̃Y��")]
    [SerializeField]
    private Vector3 m_CenterOffset;//��]�̒��S�Ƃ̃Y��
    [Header("�ʑ�/��")]
    [SerializeField]
    private float m_Phase;//�ʑ�/PI
    [Header("�ړ����x")]
    [SerializeField]
    private float m_MoveSpeed;
    private float m_Radius;//��]�̔��a
    private float m_MoveTime = 0;//�ړ�����
    private Vector3 m_Center;
    Transform m_transform;//transform�̃L���b�V��

    public Vector3 CenterOffset
    {
        get => m_CenterOffset;
        set { m_CenterOffset = value; }
    }
    public float Phase
    {
        get => m_Phase;
        set { m_Phase = value; }
    }
    public float MoveSpeed
    {
        get => m_MoveSpeed;
        set { m_MoveSpeed = value; }
    }
    public float Radius
    {
        get => m_Radius;
        set { m_Radius = value; }
    }
    public float MoveTime
    {
        get => m_MoveTime;
        set { m_MoveTime = value; }
    }
    public Vector3 Center
    {
        get => m_Center;
        set { m_Center = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
        Radius = Vector3.Distance(Vector3.zero, CenterOffset);
        Center = m_transform.localPosition + CenterOffset;
        //�����ʒu�Ɉړ�
        m_transform.localPosition = Center +
            new Vector3(Radius * Mathf.Cos(Phase * Mathf.PI), Radius * Mathf.Sin(Phase * Mathf.PI), CenterOffset.z);
    }

    // Update is called once per frame
    void Update()
    {
        //���W���X�V
        m_transform.localPosition = Center +
            new Vector3(Radius * Mathf.Cos(Phase * Mathf.PI + MoveTime), Radius * Mathf.Sin(Phase * Mathf.PI + MoveTime), CenterOffset.z);
        
        MoveTime += m_MoveSpeed * Time.deltaTime;
    }
}
