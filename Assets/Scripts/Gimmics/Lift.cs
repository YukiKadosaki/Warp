using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Lift : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D m_PlayerRB;
    private Collider2D m_Collider;
    private Transform m_transform;

    [SerializeField]
    private Vector3 m_Direction;
    [SerializeField]
    private float m_MoveSpeed;
    [SerializeField]
    private Collider2D m_RideCheckCollider;//プレイヤーが着地したかどうかのコライダー

    public Vector3 Direction
    {
        get => m_Direction;
        set { m_Direction = value; }
    }
    public float MoveSpeed
    {
        get => m_MoveSpeed;
        set { m_MoveSpeed = value; }
    }

    private void Start()
    {
        m_Collider = this.GetComponent<Collider2D>();
        m_transform = transform;
    }
    
    public void ChangeColliderTrigger(bool trigger)
    {
        m_Collider.isTrigger = trigger;
    }

    void Update()
    {
        if (MoveSpeed != 0)
        {
            m_transform.localPosition += Direction.normalized * MoveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //壁に触れると跳ね返る
        if (collision.gameObject.CompareTag("Wall"))
        {
            Direction *= -1;
        }

    }

}
