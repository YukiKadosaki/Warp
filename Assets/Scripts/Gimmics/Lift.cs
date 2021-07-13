using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Lift : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_PlayerRB;
    private Collider2D m_Collider;

    private void Start()
    {
        m_Collider = this.GetComponent<Collider2D>();
    }
    
    public void ChangeColliderTrigger(bool trigger)
    {
        m_Collider.isTrigger = trigger;
    }
}
