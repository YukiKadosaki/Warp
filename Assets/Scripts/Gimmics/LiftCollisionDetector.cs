using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCollisionDetector : MonoBehaviour
{
    private Lift m_Lift;

    private void Start()
    {
        m_Lift = this.transform.parent.gameObject.GetComponent<Lift>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Lift.ChangeColliderTrigger(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Lift.ChangeColliderTrigger(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            m_Lift.ChangeColliderTrigger(false);
        }
    }

}
